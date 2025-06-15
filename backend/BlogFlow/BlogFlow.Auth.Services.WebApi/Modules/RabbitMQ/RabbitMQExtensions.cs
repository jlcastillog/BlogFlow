using BlogFlow.Auth.Services.WebApi.Helpers;
using MassTransit;

namespace BlogFlow.Auth.Services.WebApi.Modules.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
        {
            var rabibitMQSettingSections = configuration.GetSection("RabbitMQ");
            services.Configure<RabbitMQSettings>(rabibitMQSettingSections);

            var rabbitMQSettings = rabibitMQSettingSections.Get<RabbitMQSettings>();

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((ctx, cfg) =>
                {
                    cfg.Host(rabbitMQSettings.Host, rabbitMQSettings.Port, rabbitMQSettings.VirtualHost, h =>
                    {
                        h.Username(rabbitMQSettings.UserName);
                        h.Password(rabbitMQSettings.Password);
                    });
                });
            });

            return services;
        }
    }
}
