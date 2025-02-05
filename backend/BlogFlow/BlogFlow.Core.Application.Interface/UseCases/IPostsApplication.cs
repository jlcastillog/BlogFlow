using BlogFlow.Core.Transversal.Common;
using BlogFlow.Core.Application.DTO;

namespace BlogFlow.Core.Application.Interface.UseCases
{
    public interface IPostsApplication
    {
        Task<Response<bool>> InsertAsync(PostDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> UpdateAsync(string id, PostDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<Response<PostDTO>> GetAsync(string id, CancellationToken cancellationToken = default);
        Task<Response<IEnumerable<PostDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Response<int>> CountAsync(CancellationToken cancellationToken = default);
    }
}
