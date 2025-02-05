using AutoMapper;
using BlogFlow.Core.Transversal.Common;
using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Domain.Entities;

namespace BlogFlow.Core.Application.UseCases.Posts
{
    public class PostsApplication : IPostsApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostsApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> CountAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<int>();

            try
            {
                var countPosts = await _unitOfWork.Posts.CountAsync(cancellationToken);

                response.Data = countPosts;
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
                await _unitOfWork.Posts.DeleteAsync(id);

                response.Data = await _unitOfWork.Save(cancellationToken) > 0 ? true : false;

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Delete succeded!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Post doesn't exist!!";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "Post doesn't exist";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<IEnumerable<PostDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<IEnumerable<PostDTO>>();

            try
            {
                var posts = await _unitOfWork.Posts.GetAllAsync(cancellationToken);

                response.Data = _mapper.Map<IEnumerable<PostDTO>>(posts);

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

        public async Task<Response<PostDTO>> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = new Response<PostDTO>();

            try
            {
                var post = await _unitOfWork.Posts.GetAsync(id, cancellationToken);

                response.Data = _mapper.Map<PostDTO>(post);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Get succeded!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Post doesn't exist!!";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "Post doesn't exist";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> InsertAsync(PostDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var post = _mapper.Map<Post>(entity);

                if (await _unitOfWork.Posts.InsertAsync(post))
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

        public async Task<Response<bool>> UpdateAsync(string id, PostDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var postExist = await _unitOfWork.Posts.GetAsync(id, cancellationToken);

                var post = _mapper.Map<Post>(entity);

                if (postExist != null)
                {
                    postExist.Title = post.Title;
                    postExist.Description = post.Description;

                    await _unitOfWork.Posts.UpdateAsync(postExist);

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
                    response.Message = "Post doesn't exist!!";
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
