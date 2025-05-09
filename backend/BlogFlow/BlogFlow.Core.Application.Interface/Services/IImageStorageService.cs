namespace BlogFlow.Core.Application.Interface.Services
{
    public interface IImageStorageService
    {
        Task<(bool result, string Url, string PublicId)> UploadImageAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default);
        Task<(bool result, string Url, string PublicId)> UpdateImageAsync(string PublicId, Stream fileStream, string fileName, CancellationToken cancellationToken = default);
        Task<bool> RemoveImageAsync(string PublicId, CancellationToken cancellationToken = default);
    }
}
