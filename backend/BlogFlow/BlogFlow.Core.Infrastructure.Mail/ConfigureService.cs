using BlogFlow.Core.Application.Interface.Services;
using BlogFlow.Core.Infrastructure.Mail.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogFlow.Core.Infrastructure.Mail
{
    public static class ConfigureService
    {
        public static IServiceCollection AddMailServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IEmailService, EmailService>();

            return services;
        }
    }
}
