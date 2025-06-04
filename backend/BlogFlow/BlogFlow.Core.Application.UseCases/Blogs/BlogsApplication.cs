using AutoMapper;
using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Transversal.Common;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Core.Domain.Entities;
using BlogFlow.Core.Application.Interface.Services;
using BlogFlow.Core.Application.UseCases.Images;
using System;

namespace BlogFlow.Core.Application.UseCases.Blogs
{
    public class BlogsApplication: IBlogsApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IImageStorageService _imageStorageService;

        public BlogsApplication(IUnitOfWork unitOfWork, IMapper mapper, IImageStorageService imageStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _imageStorageService = imageStorageService;
        }

        public async Task<Response<int>> CountAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<int>();

            try
            {
                var countBlogs = await _unitOfWork.Blogs.CountAsync(cancellationToken);

                response.Data = countBlogs;
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> DeleteAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var blog = await _unitOfWork.Blogs.GetAsync(id, cancellationToken);

                var publicId = blog?.Image?.PublicId;

                await _unitOfWork.Blogs.DeleteAsync(id);

                if (blog?.Image?.Id == null)
                {
                    throw new Exception("Couldn't get the image!!");
                }

                await _unitOfWork.Images.DeleteAsync(blog.Image.Id.ToString());

                response.Data = await _unitOfWork.Save(cancellationToken) > 0 ? true : false;

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Delete succeded!!";

                    if (publicId != null)
                    {
                        // Now we remove the image from the storage service
                        try
                        {
                            if (!await _imageStorageService.RemoveImageAsync(publicId))
                            {
                                throw new Exception("Failed removing file to storage service!!");
                            }
                        }
                        catch (Exception exUploadImage)
                        {
                            // Warning message the image has not been removed from storage service
                        }
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Blog doesn't exist!!";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "Blog doesn't exist";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<IEnumerable<BlogDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<IEnumerable<BlogDTO>>();

            try
            {
                var blogs = await _unitOfWork.Blogs.GetAllAsync(cancellationToken);

                response.Data = _mapper.Map<IEnumerable<BlogDTO>>(blogs);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "GetAll succeded!!";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<BlogDTO>> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = new Response<BlogDTO>();

            try
            {
                var blog = await _unitOfWork.Blogs.GetAsync(id, cancellationToken);

                response.Data = _mapper.Map<BlogDTO>(blog);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Get succeded!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Blog doesn't exist!!";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "Blog doesn't exist";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> InsertAsync(BlogDTO entity, ImageStorageDTO imageStorage, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            string url = string.Empty;
            string publicId = string.Empty;
            
            try
            {
                // Upload image to storage repository
                bool resultUploadImage = false;

                using var stream = imageStorage.File.OpenReadStream();
                (resultUploadImage, url, publicId) = await _imageStorageService.UploadImageAsync(stream, imageStorage.File.FileName, cancellationToken);

                if (!resultUploadImage)
                {
                    throw new Exception("Failed uploading file to storage service!!");
                }
            }
            catch (Exception exUploadImage)
            {
                response.IsSuccess = false;
                response.Message = exUploadImage.Message;
                return response;
            }

            try
            {
                //Insert image info into repository
                var imageDto = new ImageDTO()
                {
                    PublicId = publicId,
                    Url = url,
                };

                var image = _mapper.Map<Image>(imageDto);

                if (!await _unitOfWork.Images.InsertAsync(image))
                {
                    throw new Exception("Failed inserting image!!");
                }

                var blog = _mapper.Map<Blog>(entity);
                blog.Image = image;

                //Insert blog info into repository
                if (await _unitOfWork.Blogs.InsertAsync(blog))
                {
                    response.Data = await _unitOfWork.Save(cancellationToken) > 0 ? true : false;

                    if (response.Data)
                    {
                        response.IsSuccess = true;
                        response.Message = "Insert succeded!!";
                    }
                }
                else
                {
                    throw new Exception("Failed inserting blog!!");
                }
            }
            catch (Exception ex)
            {
                //Rollback - remove image in storage service
                await _imageStorageService.RemoveImageAsync(publicId);

                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> UpdateAsync(string id, BlogDTO entity, ImageStorageDTO image, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var blogExist = await _unitOfWork.Blogs.GetAsync(id, cancellationToken);

                if (blogExist != null)
                {
                    bool saveImage = false;
                    string url = string.Empty;
                    string publicId = string.Empty;

                    if (image.File != null)
                    {
                        try
                        {
                            // Update image to storage repository
                            bool resultUpdateImage = false;

                            using var stream = image.File.OpenReadStream();
                            (resultUpdateImage, url, publicId) = await _imageStorageService.UpdateImageAsync(blogExist.Image.PublicId, stream, image.File.FileName, cancellationToken);

                            if (!resultUpdateImage)
                            {
                                throw new Exception("Failed updating file to storage service!!");
                            }

                            saveImage = true;
                        }
                        catch (Exception exUploadImage)
                        {
                            response.IsSuccess = false;
                            response.Message = exUploadImage.Message;
                            return response;
                        }
                    }

                    var blog = _mapper.Map<Blog>(entity);

                    if (saveImage)
                    {
                        blogExist.Image = blog.Image;
                        blogExist.Image.PublicId = publicId;
                        blogExist.Image.Url = url;
                    }

                    blogExist.Title = blog.Title;
                    blogExist.Description = blog.Description;
                    blogExist.Category = blog.Category;

                    await _unitOfWork.Blogs.UpdateAsync(blogExist);

                    response.Data = await _unitOfWork.Save(cancellationToken) > 0 ? true : false;

                    if (response.Data)
                    {
                        response.IsSuccess = true;
                        response.Message = "Update succeded!!";
                    }
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Blog doesn't exist!!";
                }
            }
            catch (Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
