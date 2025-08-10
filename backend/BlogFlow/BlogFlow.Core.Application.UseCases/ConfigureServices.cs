using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Core.Application.UseCases.Blogs;
using BlogFlow.Core.Application.UseCases.Followers;
using BlogFlow.Core.Application.UseCases.Images;
using BlogFlow.Core.Application.UseCases.Posts;
using BlogFlow.Core.Application.UseCases.RefreshTokens;
using BlogFlow.Core.Application.UseCases.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlogFlow.Core.Application.UseCases
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<IBlogsApplication, BlogsApplication>();
            services.AddScoped<IPostsApplication, PostsApplication>();
            services.AddScoped<IFollowersApplication, FollowersApplication>();
            services.AddScoped<IRefreshTokenApplication, RefreshTokenApplication>();
            services.AddScoped<IImageApplication, ImageApplication>();
            services.AddScoped<IImageStorageApplication, ImageStorageApplication>();
            
            return services;
        }
    }
}
