using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BlogFlow.Auth.Services.WebApi.Modules.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo()
            {
                Version = description.ApiVersion.ToString(),
                Title = "BlogFlow Auth Services",
                Description = "Authentication ASP.NET Core Web API. ",
                TermsOfService = new Uri("https://jlcastillog.github.io/portfolio/"),
                Contact = new OpenApiContact()
                {
                    Name = "Jose Luis",
                    Email = "jlcgalvez@gmail.com",
                    Url = new Uri("https://jlcastillog.github.io/portfolio/")
                },
                License = new OpenApiLicense()
                {
                    Name = "Use under MIT License",
                    Url = new Uri("https://jlcastillog.github.io/portfolio/")
                }
            };

            if (description.IsDeprecated)
            {
                info.Description += "Deprecated version.";
            }

            return info;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // add a swagger ducment for each discovered API  version
            // note: you might choose to skip or document deprecated API version differently
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }
    }
}
