using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Application.Interface.Services;
using BlogFlow.Core.Infrastructure.Persistence.Contexts;
using BlogFlow.Core.Infrastructure.Persistence.Helpers;
using BlogFlow.Core.Infrastructure.Persistence.Repositories;
using BlogFlow.Core.Infrastructure.Persistence.Services;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BlogFlow.Core.Infrastructure.Persistence
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("BlogFlowConnection"),
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
            services.AddScoped<IFollowerRepository, FollowerRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();


            return services;
        }
    }
}
