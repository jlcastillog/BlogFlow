using AutoMapper;
using BlogFlow.Auth.Application.DTO;
using BlogFlow.Auth.Application.Interface.Persistence;
using BlogFlow.Auth.Application.Interface.UseCases;
using BlogFlow.Auth.Domain.Entities;
using BlogFlow.Auth.Transversal.Common;

namespace BlogFlow.Auth.Application.UseCases.Users
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UsersApplication(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<UserResponseDTO>> Authenticate(string userName, string password, CancellationToken cancellationToken = default)
        {
            var response = new Response<UserResponseDTO>();

            try
            {
                var user = await _unitOfWork.Users.Authenticate(userName, password, cancellationToken);

                response.Data = _mapper.Map<UserResponseDTO>(user);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Authenticate succeded!!";
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "User doesn't exist!!";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "User doesn't exist";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<int>> CountAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<int>();

            try
            {
                var countUsers = await _unitOfWork.Users.CountAsync(cancellationToken);

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
                    response.Message = "User doesn't exist!!";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "User doesn't exist";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<IEnumerable<UserResponseDTO>>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var response = new Response<IEnumerable<UserResponseDTO>>();

            try
            {
                var users = await _unitOfWork.Users.GetAllAsync(cancellationToken);

                response.Data = _mapper.Map<IEnumerable<UserResponseDTO>>(users);

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

        public async Task<Response<UserResponseDTO>> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            var response = new Response<UserResponseDTO>();

            try
            {
                var user = await _unitOfWork.Users.GetAsync(id, cancellationToken);

                response.Data = _mapper.Map<UserResponseDTO>(user);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Get succeded!!";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "User doesn't exist!!";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = false;
                response.Message = "User doesn't exist";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> InsertAsync(UserDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var user = _mapper.Map<User>(entity);

                if(await _unitOfWork.Users.InsertAsync(user))
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

        public async Task<Response<bool>> UpdateAsync(string id, UserDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                var userExist = await _unitOfWork.Users.GetAsync(id, cancellationToken);

                var user = _mapper.Map<User>(entity);

                if (userExist != null)
                {
                    userExist.UserName = user.UserName;
                    userExist.Password = user.Password;
                    userExist.FirstName = user.FirstName;
                    userExist.LastName = user.LastName;
                    
                    await _unitOfWork.Users.UpdateAsync(userExist);

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
                    response.Message = "User doesn't exist!!";
                }
            }
            catch(Exception e)
            {
                response.IsSuccess = false;
                response.Message = e.Message;
            }

            return response;
        }
    }
}
