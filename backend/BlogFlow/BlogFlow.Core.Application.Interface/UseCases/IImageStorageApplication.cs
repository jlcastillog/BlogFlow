using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Transversal.Common;

namespace BlogFlow.Core.Application.Interface.UseCases
{
    public interface IImageStorageApplication
    {
        Task<Response<(string Url, string PublicId)>> UploadImageAsync(ImageStorageDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> UpdateImageAsync(ImageStorageDTO entity, CancellationToken cancellationToken = default);
        Task<Response<bool>> RemoveImageAsync(ImageStorageDTO entity, CancellationToken cancellationToken = default);
    }
}
