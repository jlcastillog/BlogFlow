using BlogFlow.APIGateway.Services.WebApi.Helpers;

namespace BlogFlow.APIGateway.Services.WebApi.Modules.HealthCheck
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var healthCheckSettings = configuration.GetSection("HealthChecks")
                                                   .Get<HealthCheckSettings>();
            var healthCheckServices = configuration.GetSection("HealthChecks:Services")
                                                   .Get<List<HealthCheckService>>();

            foreach (var service in healthCheckServices)
            {
                services.AddHealthChecks().AddUrlGroup(new Uri(service.Url), name: service.Name);
            }

            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.SetEvaluationTimeInSeconds(healthCheckSettings.EvaluationTimeInSeconds); // Check frecuency
                setup.MaximumHistoryEntriesPerEndpoint(healthCheckSettings.MaximumHistoryEntriesPerEndpoint); // History by endpoint
                setup.AddHealthCheckEndpoint("API Gateway", "/health"); 
                foreach (var service in healthCheckServices)
                {
                    setup.AddHealthCheckEndpoint(service.Name, service.Url);
                }
            }).AddInMemoryStorage(); 

            return services;
        }
    }
}
