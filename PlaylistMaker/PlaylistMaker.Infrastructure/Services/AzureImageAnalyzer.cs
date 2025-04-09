using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Azure;
using Azure.AI.Vision.ImageAnalysis;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;
using PlaylistMaker.Domain.Entities;
using PlaylistMaker.Domain.Interfaces;

namespace PlaylistMaker.Infrastructure.Services
{
    public class AzureImageAnalyzer : IImageAnalyzer
    {
        private readonly string _endpoint;
        private readonly string _key;

        public AzureImageAnalyzer()
        {
            _endpoint = Environment.GetEnvironmentVariable("VISION_ENDPOINT");
            _key = Environment.GetEnvironmentVariable("VISION_KEY");
        }

        public async Task<AzureImageAnalysisResult> AnalyzeImageAsync(Stream imageStream)
        {
            ImageAnalysisClient client = new ImageAnalysisClient(
                new Uri(_endpoint),
                new AzureKeyCredential(_key));

            ImageAnalysisResult result = client.Analyze(
                BinaryData.FromStream(imageStream),
                VisualFeatures.People | VisualFeatures.Read | VisualFeatures.Tags,
                new ImageAnalysisOptions { GenderNeutralCaption = true });

            Console.WriteLine(result.Tags);

            var analysisResult = new AzureImageAnalysisResult
            {
                Caption = result.Caption?.Text,
                Tags = new List<string>()
            };

            return analysisResult;
        }
    }
}
