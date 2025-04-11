using PlaylistMaker.Domain.Entities;

namespace PlaylistMaker.Domain.Interfaces
{
    public interface IImageAnalyzer
    {
        Task<AzureImageAnalysisResult> AnalyzeImageAsync(Stream imageStream);
    }
}