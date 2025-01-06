using AutoMapper;
using BlogFlow.Auth.Application.DTO;
using BlogFlow.Auth.Application.Interface.Persistence;
using BlogFlow.Auth.Application.Interface.UseCases;
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

        public Response<UserDTO> Authenticate(string userName, string password, CancellationToken cancellationToken = default)
        {
            var response = new Response<UserDTO>();

            try
            {
                var user = _unitOfWork.Users.Authenticate(userName, password, cancellationToken);

                response.Data = _mapper.Map<UserDTO>(user);

                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Authenticate succeded!!";
                }
            }
            catch (InvalidOperationException)
            {
                response.IsSuccess = true;
                response.Message = "User doesn't exist";
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
