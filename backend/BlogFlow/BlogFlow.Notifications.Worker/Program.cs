using BlogFlow.Core.Application.Interface.Services;
using BlogFlow.Core.Infrastructure.Mail.Services;
using BlogFlow.Notifications.Worker.Consumer;
using BlogFlow.Notifications.Worker.Helpers;
using MassTransit;
using Serilog;

var host = Host.CreateDefaultBuilder(args)
    .UseSerilog((context, services, loggerConfiguration) =>
    {
        loggerConfiguration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext();
    })
    .ConfigureServices((context, services) =>
    {
        var configuration = context.Configuration;

        var rabibitMQSettingSections = configuration.GetSection("RabbitMQ");
        services.Configure<RabbitMQSettings>(rabibitMQSettingSections);

        var rabbitMQSettings = rabibitMQSettingSections.Get<RabbitMQSettings>();

        services.AddSingleton<IEmailService, EmailService>();

        services.AddMassTransit(x =>
        {
            x.AddConsumer<UserRegisteredConsumer>();

            x.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(rabbitMQSettings.Host, rabbitMQSettings.Port, rabbitMQSettings.VirtualHost, h =>
                {
                    h.Username(rabbitMQSettings.UserName);
                    h.Password(rabbitMQSettings.Password);
                });

                cfg.ReceiveEndpoint("user-registered-queue", e =>
                {
                    e.ConfigureConsumer<UserRegisteredConsumer>(ctx);
                });
            });
        });
    })
    .Build();


host.Run();
