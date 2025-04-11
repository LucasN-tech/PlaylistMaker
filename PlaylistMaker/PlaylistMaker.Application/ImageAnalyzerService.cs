using PlaylistMaker.Domain.Entities;
using PlaylistMaker.Domain.Interfaces;

namespace PlaylistMaker.Application.Services
{
    public class ImageAnalyzerService
    {
        private readonly IImageAnalyzer _imageAnalyzer;

        public ImageAnalyzerService(IImageAnalyzer imageAnalyzer)
        {
            _imageAnalyzer = imageAnalyzer;
        }

        public async Task<AzureImageAnalysisResult> AnalyzeAsync(Stream image)
        {
            return await _imageAnalyzer.AnalyzeImageAsync(image);
        }
    }
}
