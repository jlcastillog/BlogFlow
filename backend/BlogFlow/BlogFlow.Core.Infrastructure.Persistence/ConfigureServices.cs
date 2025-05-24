using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Application.Interface.Services;
using BlogFlow.Core.Infrastructure.Persistence.Contexts;
using BlogFlow.Core.Infrastructure.Persistence.Helpers;
using BlogFlow.Core.Infrastructure.Persistence.Repositories;
using BlogFlow.Core.Infrastructure.Persistence.Services;
using BlogFlow.Core.Transversal.Secrets;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BlogFlow.Core.Infrastructure.Persistence
{
    public static class ConfigureServices
    {
        public static async Task<IServiceCollection> AddPersistenceServicesAsync(this IServiceCollection services, IConfiguration configuration)
        {
            string dataBaseConnectionString = string.Empty;
            var secretManager = services.BuildServiceProvider().GetRequiredService<ISecretManager>();

            if (configuration.GetValue<bool>("IsVaultSecretEnable"))
            {
                var secret = await secretManager.Get<SqlServerCredentials>("BlogFlowConnection");

                dataBaseConnectionString = secret.ConnectionString;
            }
            else
            {
                dataBaseConnectionString = configuration.GetConnectionString("BlogFlowConnection");
            }

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(dataBaseConnectionString,
                                     builder =>
                                         {
                                             builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                                             builder.EnableRetryOnFailure(5, System.TimeSpan.FromSeconds(10), null);
                                         }
                                     );
            });

            var cloudinarySettingsSection = configuration.GetSection("CloudinarySettings");
            services.Configure<CloudinarySettings>(cloudinarySettingsSection);

            services.AddSingleton(provider =>
            {
                var config = provider.GetRequiredService<IOptions<CloudinarySettings>>().Value;
                var account = new Account(config.CloudName, config.ApiKey, config.ApiSecret);
                return new Cloudinary(account);
            });

            services.AddScoped<IImageStorageService, CloudinaryImageStorageService>();

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IBlogsRepository, BlogsRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IImageRepository, ImageRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private record SqlServerCredentials
        {
            public string ConnectionString { get; init; } = null!;
        }
    }
}
