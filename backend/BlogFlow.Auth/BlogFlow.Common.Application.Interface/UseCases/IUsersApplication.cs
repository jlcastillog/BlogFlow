using BlogFlow.Auth.Application.DTO;
using BlogFlow.Auth.Transversal.Common;

namespace BlogFlow.Common.Application.Interface.UseCases
{
    public interface IUsersApplication
    {
        Task<Response<UserResponseDTO>> Authenticate(string userName, string password, CancellationToken cancellationToken = default);
        Task<Response<bool>> InsertAsync(UserDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> UpdateAsync(string id, UserDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<Response<UserResponseDTO>> GetAsync(string id, CancellationToken cancellationToken = default);
        Task<Response<IEnumerable<UserResponseDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Response<int>> CountAsync(CancellationToken cancellationToken = default);
    }
}
