﻿using BlogFlow.APIGateway.Services.WebApi.Helpers;
using BlogFlow.Core.Application.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text;

namespace BlogFlow.APIGateway.Services.WebApi.Modules.Authentication
{
    public static class AuthenticationExtensions
    {
        public static IServiceCollection AddAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            var appSettingSections = configuration.GetSection("Config");
            services.Configure<AppSettings>(appSettingSections);

            //Configure jwt authetication
            var appSettings = appSettingSections.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var issuer = appSettings.Issuer;
            var audience = appSettings.Audience;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // Leer el JWT desde la cookie
                        if (context.Request.Cookies.ContainsKey("jwt"))
                        {
                            context.Token = JsonConvert.DeserializeObject<RefreshTokenDTO>(context.Request.Cookies["jwt"])?.AccessToken;
                        }
                        return Task.CompletedTask;
                    },

                    OnTokenValidated = context =>
                    {
                        var userId = int.Parse(context.Principal.Identity.Name);
                        return Task.CompletedTask;
                    },

                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("Token-Expired", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = false;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = true,
                    ValidAudience = audience,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AllowAnonymous", policy => policy.RequireAssertion(_ => true));
                options.AddPolicy("Authenticated", policy =>
                {
                    policy.RequireAuthenticatedUser(); 
                });
            });

            return services;
        }
    }
}
