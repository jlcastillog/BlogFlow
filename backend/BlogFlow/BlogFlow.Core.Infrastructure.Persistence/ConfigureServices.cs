using BlogFlow.Core.Application.Interface.Persistence;
using BlogFlow.Core.Infrastructure.Persistence.Contexts;
using BlogFlow.Core.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IBlogsRepository, BlogsRepository>();
            services.AddScoped<IPostsRepository, PostsRepository>();
            services.AddScoped<IContentsRepository, ContentsRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
