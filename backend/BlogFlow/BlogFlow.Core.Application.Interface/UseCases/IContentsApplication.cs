using BlogFlow.Core.Transversal.Common;
using BlogFlow.Core.Application.DTO;

namespace BlogFlow.Core.Application.Interface.UseCases
{
    public interface IContentsApplication
    {
        Task<Response<bool>> InsertAsync(ContentDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> UpdateAsync(string id, ContentDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<Response<ContentDTO>> GetAsync(string id, CancellationToken cancellationToken = default);
        Task<Response<IEnumerable<ContentDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Response<int>> CountAsync(CancellationToken cancellationToken = default);
    }
}
