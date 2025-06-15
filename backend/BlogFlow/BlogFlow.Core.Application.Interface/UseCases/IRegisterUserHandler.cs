using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Transversal.Common;

namespace BlogFlow.Core.Application.Interface.UseCases
{
    public interface IRegisterUserHandler
    {
        Task<Response<bool>> HandleAsync(UserDTO userDto, CancellationToken cancellationToken = default);
    }
}
