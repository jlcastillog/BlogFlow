using AutoMapper;
using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Core.Domain.Entities;
using BlogFlow.Core.Transversal.Common;

namespace BlogFlow.Core.Application.UseCases.Followers
{
    public class FollowersApplication : IFollowersApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FollowersApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<int>> CountAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<int>();
            try
            {
                var count = await _unitOfWork.Followers.CountAsync(cancellationToken);
                response.Data = count;
                response.IsSuccess = true;
                response.Message = "Count succeeded!!";
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
                var follower = await _unitOfWork.Followers.GetAsync(id, cancellationToken);
                if (follower == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Follower doesn't exist!!";
                    response.Data = false;
                    return response;
                }

                var result = await _unitOfWork.Followers.DeleteAsync(id);
                response.Data = await _unitOfWork.Save(cancellationToken) > 0 && result;

                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Delete succeeded!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Follower couldn't be deleted!!";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<IEnumerable<FollowerDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<IEnumerable<FollowerDTO>>();
            try
            {
                var followers = await _unitOfWork.Followers.GetAllAsync(cancellationToken);
                response.Data = _mapper.Map<IEnumerable<FollowerDTO>>(followers);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "GetAll succeeded!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "No followers found!!";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<FollowerDTO>> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = new Response<FollowerDTO>();
            try
            {
                var follower = await _unitOfWork.Followers.GetAsync(id, cancellationToken);
                response.Data = _mapper.Map<FollowerDTO>(follower);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Get succeeded!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Follower doesn't exist!!";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<bool>> InsertAsync(FollowerDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {
                var follower = _mapper.Map<Follower>(entity);
                var result = await _unitOfWork.Followers.InsertAsync(follower);
                response.Data = await _unitOfWork.Save(cancellationToken) > 0 && result;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Insert succeeded!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Failed inserting follower!!";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<bool>> UpdateAsync(string id, FollowerDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {
                var followerExist = await _unitOfWork.Followers.GetAsync(id, cancellationToken);
                if (followerExist == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Follower doesn't exist!!";
                    response.Data = false;
                    return response;
                }

                var follower = _mapper.Map<Follower>(entity);
                follower.Id = int.Parse(id);
                var result = await _unitOfWork.Followers.UpdateAsync(follower);
                response.Data = await _unitOfWork.Save(cancellationToken) > 0 && result;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Update succeeded!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Failed updating follower!!";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
