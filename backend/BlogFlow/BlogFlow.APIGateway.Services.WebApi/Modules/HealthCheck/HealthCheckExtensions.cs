using BlogFlow.APIGateway.Services.WebApi.Helpers;

namespace BlogFlow.APIGateway.Services.WebApi.Modules.HealthCheck
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var healthCheckSettings = configuration.GetSection("HealthChecks:Services")
                                                   .Get<List<HealthCheckService>>();

            var healthChecks = services.AddHealthChecks();

            foreach (var service in healthCheckSettings)
            {
                healthChecks.AddUrlGroup(new Uri(service.Url), name: service.Name);
            }

            return services;
        }
    }
}
