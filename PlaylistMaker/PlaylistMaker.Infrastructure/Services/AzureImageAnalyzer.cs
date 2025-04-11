using Azure;
using Azure.AI.Vision.Face;
using Azure.AI.Vision.ImageAnalysis;
using Microsoft.Extensions.Options;
using PlaylistMaker.Domain.Entities;
using PlaylistMaker.Domain.Interfaces;

public class AzureImageAnalyzer : IImageAnalyzer
{
    private readonly string _visionEndpoint;
    private readonly string _visionKey;
    public AzureImageAnalyzer(IOptions<AzureVisionSettings> visionConfig, IOptions<AzureFaceSettings> faceConfig)
    {
        _visionEndpoint = visionConfig.Value.Endpoint;
        _visionKey = visionConfig.Value.Key;
    }

    public async Task<AzureImageAnalysisResult> AnalyzeImageAsync(Stream imageStream)
    {
        var imageBytes = await GetBytes(imageStream);
        var imageResult = await AnalyzeWithVision(imageBytes);

        return new AzureImageAnalysisResult
        {
            DenseCaption = imageResult.Caption,
            Tags = imageResult.Tags,
            Objects = imageResult.Objects,
        };
    }

    private async Task<(string Caption, List<string> Tags, List<string> Objects)> AnalyzeWithVision(byte[] imageBytes)
    {
        var client = new ImageAnalysisClient(new Uri(_visionEndpoint), new AzureKeyCredential(_visionKey));
        using var stream = new MemoryStream(imageBytes);

        var response = await client.AnalyzeAsync(
            BinaryData.FromStream(stream),
            VisualFeatures.DenseCaptions | VisualFeatures.Tags | VisualFeatures.Objects
        );

        var result = response.Value;
        var caption = result.DenseCaptions?.Values?.FirstOrDefault()?.Text ?? "";

        var tags = result.Tags?.Values.Select(t => t.Name).ToList() ?? new();
        var objects = result.Objects?.Values
            .Select(o => o.Tags.FirstOrDefault()?.Name ?? "")
            .Where(name => !string.IsNullOrEmpty(name))
            .ToList() ?? new();

        return (caption, tags, objects);
    }
    private async Task<byte[]> GetBytes(Stream stream)
    {
        using var ms = new MemoryStream();
        await stream.CopyToAsync(ms);
        return ms.ToArray();
    }
}