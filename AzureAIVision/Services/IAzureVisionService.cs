using Azure;
using Azure.AI.Vision.ImageAnalysis;
using AzureAIVision.Options;
using Microsoft.Extensions.Options;

namespace AzureAIVision.Services
{
    public interface IAzureVisionService
    {
        ImageAnalysisResult AnalizeImage(Stream stream);
    }

    public class AzureVisionService : IAzureVisionService
    {
        private readonly ImageAnalysisClient imageAnalysisClient;

        public AzureVisionService(IOptions<AzureVisionSettings> options)
        {
            var settings = options.Value;

            this.imageAnalysisClient = new ImageAnalysisClient(
                new Uri(settings.Endpoint),
                new AzureKeyCredential(settings.ApiKey));
        }

        public ImageAnalysisResult AnalizeImage(Stream stream)
        {
            return imageAnalysisClient.Analyze(
                BinaryData.FromStream(stream),
                VisualFeatures.Caption |
                VisualFeatures.DenseCaptions |
                VisualFeatures.Tags |
                VisualFeatures.Objects
                );
        }
    }
}
