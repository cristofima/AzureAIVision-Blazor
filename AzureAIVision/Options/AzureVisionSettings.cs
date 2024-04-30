namespace AzureAIVision.Options
{
    public class AzureVisionSettings
    {
        public string ApiKey { get; set; }
        public string Endpoint { get; set; }
        public CustomVision Classification { get; set; }
        public CustomVision Detection { get; set; }
    }

    public class CustomVision
    {
        public string ProjectId { get; set; }
        public string PredictionKey { get; set; }
        public string Endpoint { get; set; }
        public string PublishedModelName { get; set; }
    }
}
