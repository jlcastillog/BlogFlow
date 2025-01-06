using BlogFlow.Auth.Application.DTO;
using BlogFlow.Auth.Transversal.Common;

namespace BlogFlow.Auth.Application.Interface.UseCases
{
    public interface IUsersApplication
    {
        Response<UserDTO> Authenticate(string userName, string password, CancellationToken cancellationToken = default);
    }
}
