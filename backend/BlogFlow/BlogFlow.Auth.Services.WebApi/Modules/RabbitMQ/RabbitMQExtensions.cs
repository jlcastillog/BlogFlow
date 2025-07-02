using BlogFlow.Auth.Services.WebApi.Helpers;
using MassTransit;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace BlogFlow.Auth.Services.WebApi.Modules.RabbitMQ
{
    public static class RabbitMQExtensions
    {
        public static async Task<IServiceCollection> AddRabbitMQAsync(this IServiceCollection services, IConfiguration configuration)
        {
            var rabibitMQSettingSections = configuration.GetSection("RabbitMQ");
            services.Configure<RabbitMQSettings>(rabibitMQSettingSections);

            var rabbitMQSettings = rabibitMQSettingSections.Get<RabbitMQSettings>();

            await CheckRabbitMQStatusConnection(rabbitMQSettings.Host,
                                                rabbitMQSettings.UserName,
                                                rabbitMQSettings.Password);

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

        private static async Task CheckRabbitMQStatusConnection(string host, string userName, string password)
        {
            var factory = new ConnectionFactory
            {
                HostName = host,
                UserName = userName,
                Password = password
            };

            const int maxRetries = 5;
            int retries = 0;

            while (true)
            {
                try
                {
                    using var connection = await factory.CreateConnectionAsync();
                    using var channel = await connection.CreateChannelAsync();
                    Console.WriteLine("✅ Connected to RabbitMQ.");
                    break; // Success
                }
                catch (BrokerUnreachableException ex)
                {
                    retries++;
                    Console.WriteLine($"❌ Error connecting to RabbitMQ (try {retries}): {ex.Message}");
                    if (retries >= maxRetries)
                    {
                        Console.WriteLine("⛔ Max retires reached. Aborting...");
                        throw;
                    }

                    await Task.Delay(5000);
                }
            }
        }
     }
}
