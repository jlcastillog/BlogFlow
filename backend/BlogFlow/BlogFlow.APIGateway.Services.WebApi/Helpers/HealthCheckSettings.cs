namespace BlogFlow.APIGateway.Services.WebApi.Helpers
{
    public class HealthCheckSettings
    {
        public List<HealthCheckService> Services { get; set; }
    }

    public class HealthCheckService
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
