using BlogFlow.Auth.Application.Interface.UseCases;
using BlogFlow.Auth.Application.UseCases.Users;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlogFlow.Auth.Application.UseCases
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<IUsersApplication, UsersApplication>();

            return services;
        }
    }
}
