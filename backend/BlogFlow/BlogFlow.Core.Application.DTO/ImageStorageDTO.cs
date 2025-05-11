using Microsoft.AspNetCore.Http;

namespace BlogFlow.Core.Application.DTO
{
    public class ImageStorageDTO
    {
        public IFormFile File { get; set; }
        public string PublicId { get; set; }
    }
}
