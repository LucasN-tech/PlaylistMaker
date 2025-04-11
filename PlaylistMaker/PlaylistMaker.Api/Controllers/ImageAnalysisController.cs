using Microsoft.AspNetCore.Mvc;
using PlaylistMaker.Application.Services;
using PlaylistMaker.Application;

namespace PlaylistMaker.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageAnalysisController : ControllerBase
    {
        private readonly ImageAnalyzerService _service;

        public ImageAnalysisController(ImageAnalyzerService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Analyze([FromForm] ImageUploadRequest request)
        {
            if (request.Image == null || request.Image.Length == 0)
                return BadRequest("Image file is required.");

            using var stream = request.Image.OpenReadStream();
            var result = await _service.AnalyzeAsync(stream);
            return Ok(result);
        }
    }
}