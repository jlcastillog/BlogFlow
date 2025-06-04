using BlogFlow.Auth.Services.WebApi.Helpers;
using System.Text.Json.Serialization;

namespace BlogFlow.Auth.Services.WebApi.Modules.Feature
{
    public static class FeatureExtension
    {
        public static string myPolicy = "policyApiBlogFlow";

        public static IServiceCollection AddFeature(this IServiceCollection services, IConfiguration configuration)
        {
            var appSettingSections = configuration.GetSection("Config");
            services.Configure<AppSettings>(appSettingSections);

            services.AddCors(options => options.AddPolicy(myPolicy, policy => policy.WithOrigins(configuration.GetSection("Config:OriginCors").Get<string[]>())
                                                                                    .AllowAnyHeader()
                                                                                    .AllowAnyMethod()
                                                                                    .AllowCredentials()));


            services.AddControllers();

            return services;
        }
    }
}
