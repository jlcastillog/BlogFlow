using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Transversal.Common;

namespace BlogFlow.Core.Application.Interface.UseCases
{
    public interface IRefreshTokenApplication
    {
        Task<Response<bool>> InsertAsync(RefreshTokenDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> ExistsRefreshAsync(RefreshTokenDTO token, CancellationToken cancellationToken = default);
        Task<Response<bool>> MarkRefreshTokenAsUsed(RefreshTokenDTO token, CancellationToken cancellationToken = default);

    }
}
