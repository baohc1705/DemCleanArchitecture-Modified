using DemoCleanArchitectureV2.Domain.Interfaces;
using DemoCleanArchitectureV2.Infrastructure.Data;
using DemoCleanArchitectureV2.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DemoCleanArchitectureV2.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureService(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddDbContext<AppDbContext>(options => {
                options.UseLazyLoadingProxies().UseSqlServer(configuration.GetConnectionString("MenuNewsDB"));
            });

            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<INewsMenuRepository, MenuNewsRepository>();
            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));
            return services;
        }
    }
}
