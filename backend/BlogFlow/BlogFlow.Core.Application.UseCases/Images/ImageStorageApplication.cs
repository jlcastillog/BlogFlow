using BlogFlow.Core.Application.DTO;
using BlogFlow.Core.Application.Interface.Services;
using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Core.Transversal.Common;

namespace BlogFlow.Core.Application.UseCases.Images
{
    public class ImageStorageApplication : IImageStorageApplication
    {
        private readonly IImageStorageService _imageStorageService;

        public ImageStorageApplication(IImageStorageService imageStorageService)
        {
            _imageStorageService = imageStorageService;
        }

        public async Task<Response<bool>> RemoveImageAsync(ImageStorageDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                if (await _imageStorageService.RemoveImageAsync(entity.PublicId))
                {
                    response.Data = true;
                    response.IsSuccess = true;
                }
                else
                {
                    response.Data = false;
                    response.IsSuccess = false;
                    response.Message = "Failed removing image";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<bool>> UpdateImageAsync(ImageStorageDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();

            try
            {
                using var stream = entity.File.OpenReadStream();
                var (result, url, publicId) = await _imageStorageService.UpdateImageAsync(entity.PublicId, stream, entity.File.FileName);
                if (result)
                {
                    response.Data = true;
                    response.IsSuccess = true;
                }
                else
                {
                    response.Data = false;
                    response.IsSuccess = false;
                    response.Message = "Failed updating image";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<Response<(string Url, string PublicId)>> UploadImageAsync(ImageStorageDTO entity, CancellationToken cancellationToken = default)
        {
            var response = new Response<(string Url, string PublicId)>();

            try
            {
                using var stream = entity.File.OpenReadStream();
                var (result, url, publicId) = await _imageStorageService.UploadImageAsync(stream, entity.File.FileName, cancellationToken);

                response.Data = (url, publicId);
                response.IsSuccess = true;
            }
            catch (Exception ex) 
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
           
            return response;
        }
    }
}
