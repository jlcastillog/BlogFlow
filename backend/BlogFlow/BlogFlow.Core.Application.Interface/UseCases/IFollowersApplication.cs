using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Transversal.Common;

namespace BlogFlow.Core.Application.Interface.UseCases
{
    public interface IFollowersApplication
    {
        Task<Response<bool>> InsertAsync(FollowerDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> UpdateAsync(string id, FollowerDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<Response<FollowerDTO>> GetAsync(string id, CancellationToken cancellationToken = default);
        Task<Response<IEnumerable<FollowerDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Response<int>> CountAsync(CancellationToken cancellationToken = default);
    }
}
