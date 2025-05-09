using AutoMapper;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Core.Domain.Entities;
using BlogFlow.Core.Transversal.Common;

namespace BlogFlow.Core.Application.UseCases.Images
{
    public class ImageApplication : IImageApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ImageApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> CountAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<int>();

            try
            {
                var countImages = await _unitOfWork.Images.CountAsync(cancellationToken);

                response.Data = countImages;
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
                await _unitOfWork.Images.DeleteAsync(id);

                response.Data = await _unitOfWork.Save(cancellationToken) > 0 ? true : false;

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Delete succeded!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Image doesn't exist!!";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "Image doesn't exist";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<IEnumerable<ImageDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<IEnumerable<ImageDTO>>();

            try
            {
                var images = await _unitOfWork.Images.GetAllAsync(cancellationToken);

                response.Data = _mapper.Map<IEnumerable<ImageDTO>>(images);

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

        public async Task<Response<ImageDTO>> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = new Response<ImageDTO>();

            try
            {
                var image = await _unitOfWork.Images.GetAsync(id, cancellationToken);

                response.Data = _mapper.Map<ImageDTO>(image);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Get succeded!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Image doesn't exist!!";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "Image doesn't exist";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<ImageDTO>> InsertAsync(ImageDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<ImageDTO>();

            try
            {
                var image = _mapper.Map<Image>(entity);

                if (await _unitOfWork.Images.InsertAsync(image))
                {
                    response.IsSuccess = await _unitOfWork.Save(cancellationToken) > 0 ? true : false;

                    if (response.IsSuccess)
                    {
                        response.Data = entity;
                        response.Message = "Insert succeded!!";
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> UpdateAsync(string id, ImageDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var imageExist = await _unitOfWork.Images.GetAsync(id, cancellationToken);

                var image = _mapper.Map<Image>(entity);

                if (imageExist != null)
                {
                    imageExist.Url = image.Url;
                    imageExist.PublicId = image.PublicId;

                    await _unitOfWork.Images.UpdateAsync(imageExist);

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
                    response.Message = "Image doesn't exist!!";
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
