using BlogFlow.Common.Application.Interface.UserCases;
using BlogFlow.Core.Application.UseCases.Blogs;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlogFlow.Core.Application.UseCases
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IBlogsApplication, BlogsApplicaction>();

            return services;
        }
    }
}
