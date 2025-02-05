using BlogFlow.Core.Application.Interface.UseCases;
using BlogFlow.Core.Application.UseCases.Blogs;
using BlogFlow.Core.Application.UseCases.Content;
using BlogFlow.Core.Application.UseCases.Posts;
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
            services.AddScoped<IBlogsApplication, BlogsApplicaction>();
            services.AddScoped<IPostsApplication, PostsApplication>();
            services.AddScoped<IContentsApplication, ContentsApplication>();

            return services;
        }
    }
}
