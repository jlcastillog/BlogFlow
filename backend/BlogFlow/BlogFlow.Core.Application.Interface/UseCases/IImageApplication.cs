using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Transversal.Common;

namespace BlogFlow.Core.Application.Interface.UseCases
{
    public interface IImageApplication
    {
        Task<Response<ImageDTO>> InsertAsync(ImageDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> UpdateAsync(string id, ImageDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> DeleteAsync(string id, CancellationToken cancellationToken = default);
        Task<Response<ImageDTO>> GetAsync(string id, CancellationToken cancellationToken = default);
        Task<Response<IEnumerable<ImageDTO>>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<Response<int>> CountAsync(CancellationToken cancellationToken = default);
    }
}
