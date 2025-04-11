using Microsoft.AspNetCore.Http;

namespace PlaylistMaker.Application
{
    public class ImageUploadRequest
    {
        public IFormFile Image { get; set; }
    }
}
