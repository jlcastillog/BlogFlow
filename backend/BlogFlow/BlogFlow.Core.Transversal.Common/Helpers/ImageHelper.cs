using Microsoft.AspNetCore.Http;

namespace BlogFlow.Core.Transversal.Common.Helpers
{
    public static class ImageHelper
    {
        public static void IFormFileToByteArray(IFormFile image, out byte[] imageBytes)
        {
            using var memoryStream = new MemoryStream();
            image.CopyTo(memoryStream);
            imageBytes = memoryStream.ToArray();
        }
    }
}
