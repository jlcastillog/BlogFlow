using AutoMapper;
using BlogFlow.Common.Application.Interface.Persistence;
using BlogFlow.Auth.Transversal.Common;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Common.Application.Interface.UserCases;
using BlogFlow.Auth.Application.DTO;
using BlogFlow.Auth.Domain.Entities;
using BlogFlow.Core.Domain.Entities;

namespace BlogFlow.Core.Application.UseCases.Blogs
{
    public class BlogsApplicaction: IBlogsApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BlogsApplicaction(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> CountAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<int>();

            try
            {
                var countUsers = await _unitOfWork.Blogs.CountAsync(cancellationToken);

                response.Data = countUsers;
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
                await _unitOfWork.Users.DeleteAsync(id);

                response.Data = await _unitOfWork.Save(cancellationToken) > 0 ? true : false;

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Delete succeded!!";
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
                var users = await _unitOfWork.Blogs.GetAllAsync(cancellationToken);

                response.Data = _mapper.Map<IEnumerable<BlogDTO>>(users);

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
                var user = await _unitOfWork.Blogs.GetAsync(id, cancellationToken);

                response.Data = _mapper.Map<BlogDTO>(user);

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

        public async Task<Response<bool>> InsertAsync(BlogDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var blog = _mapper.Map<Blog>(entity);

                if (await _unitOfWork.Blogs.InsertAsync(blog))
                {
                    response.Data = await _unitOfWork.Save(cancellationToken) > 0 ? true : false;

                    if (response.Data)
                    {
                        response.IsSuccess = true;
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

        public async Task<Response<bool>> UpdateAsync(string id, BlogDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var blogExist = await _unitOfWork.Blogs.GetAsync(id, cancellationToken);

                var blog = _mapper.Map<Blog>(entity);

                if (blogExist != null)
                {
                    blogExist.Title = blog.Title;
                    blogExist.Description = blog.Description;

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
