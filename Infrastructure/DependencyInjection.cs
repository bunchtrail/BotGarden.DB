using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Регистрация контекста базы данных
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("DefaultConnection"),
                    o => o.UseNetTopologySuite() // Включаем поддержку PostGIS
                          .MigrationsAssembly("Infrastructure")));

            // Регистрация репозиториев
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IPlantRepository, PlantRepository>();
            services.AddScoped<IAreaRepository, AreaRepository>();

            return services;
        }
    }
} 