using Microsoft.Extensions.DependencyInjection;

namespace BlogFlow.Auth.Services.WebApi.Modules.HealthChecks
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("BlogFlowConnection"), tags: new[] { "database" });

            return services;
        }
    }
}
