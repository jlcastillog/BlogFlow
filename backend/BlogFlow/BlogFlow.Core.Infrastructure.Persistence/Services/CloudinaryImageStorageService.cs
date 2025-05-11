using BlogFlow.Core.Application.Interface.Services;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BlogFlow.Core.Infrastructure.Persistence.Services
{
    public class CloudinaryImageStorageService : IImageStorageService
    {
        private readonly Cloudinary _cloudinary;

        public CloudinaryImageStorageService(Cloudinary cloudinary)
        {
            _cloudinary = cloudinary;
        }

        public async Task<bool> RemoveImageAsync(string PublicId, CancellationToken cancellationToken = default)
        {
            var deleteParams = new DeletionParams(PublicId);
            var removeResult = await _cloudinary.DestroyAsync(deleteParams);
            return removeResult.StatusCode == System.Net.HttpStatusCode.OK;
        }

        public async Task<(bool result, string Url, string PublicId)> UpdateImageAsync(string PublicId, Stream fileStream, string fileName, CancellationToken cancellationToken = default)
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(fileName, fileStream),
                PublicId = PublicId,
                Overwrite = true
            };

            var updateResult = await _cloudinary.UploadAsync(uploadParams);

            return (updateResult.StatusCode == System.Net.HttpStatusCode.OK, updateResult.SecureUrl.ToString(), updateResult.PublicId);
        }

        public async Task<(bool result, string Url, string PublicId)> UploadImageAsync(Stream fileStream, string fileName, CancellationToken cancellationToken = default)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, fileStream),
                Folder = "blogflow-images"
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams, cancellationToken);

            return (uploadResult.StatusCode == System.Net.HttpStatusCode.OK, uploadResult.SecureUrl.ToString(), uploadResult.PublicId);
        }
    }
}
