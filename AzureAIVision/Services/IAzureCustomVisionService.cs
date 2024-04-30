using AzureAIVision.Options;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using Microsoft.Extensions.Options;

namespace AzureAIVision.Services
{
    public interface IAzureCustomVisionService
    {
        IList<PredictionModel> ClassifyImage(Stream image);
        IList<PredictionModel> DetectObjects(Stream image);
    }

    public class AzureCustomVisionService : IAzureCustomVisionService
    {
        private readonly CustomVisionPredictionClient predictionApi;
        private readonly AzureVisionSettings settings;

        public AzureCustomVisionService(IOptions<AzureVisionSettings> options)
        {
            this.settings = options.Value;
            this.predictionApi = new CustomVisionPredictionClient(new ApiKeyServiceClientCredentials(this.settings.Classification.PredictionKey))
            {
                Endpoint = this.settings.Classification.Endpoint
            };
        }

        public IList<PredictionModel> ClassifyImage(Stream image)
        {
            var result = this.predictionApi.ClassifyImage(Guid.Parse(this.settings.Classification.ProjectId), this.settings.Classification.PublishedModelName, image);
            return result.Predictions;
        }

        public IList<PredictionModel> DetectObjects(Stream image)
        {
            var result = this.predictionApi.DetectImage(Guid.Parse(this.settings.Detection.ProjectId), this.settings.Detection.PublishedModelName, image);
            return result.Predictions;
        }
    }
}
