using BlogFlow.Auth.Application.DTO;
using BlogFlow.Auth.Transversal.Common;

namespace BlogFlow.Auth.Application.Interface.UseCases
{
    public interface IUsersApplication
    {
        Task<Response<UserDTO>> Authenticate(string userName, string password, CancellationToken cancellationToken = default);
    }
}
