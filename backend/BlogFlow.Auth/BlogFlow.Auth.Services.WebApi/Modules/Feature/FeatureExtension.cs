using System.Text.Json.Serialization;

namespace BlogFlow.Auth.Services.WebApi.Modules.Feature
{
    public static class FeatureExtension
    {
        public static string myPolicy = "policyApiBlogFlow";

        public static IServiceCollection AddFeature(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options => options.AddPolicy(myPolicy, policy => policy.WithOrigins(configuration.GetSection("Config:OriginCors").Value)
                                                                                .AllowAnyHeader()
                                                                                .AllowAnyMethod()));


            services.AddControllers();

            return services;
        }
    }
}
