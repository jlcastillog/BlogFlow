﻿using Serilog;

namespace BlogFlow.Auth.Services.WebApi.Modules.Logger
{
    public static class LoggerExtension
    {
        public static WebApplicationBuilder AddLogger(this WebApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                                    .ReadFrom.Configuration(builder.Configuration)
                                    .CreateLogger();

            builder.Host.UseSerilog();

            return builder;
        }
    }
}
