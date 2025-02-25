using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

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

        // Метод для инициализации базы данных при запуске приложения
        public static async Task InitializeDatabaseAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<AppDbContext>>();

            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                
                // Применяем миграции
                await context.Database.MigrateAsync();
                
                // Инициализируем базу данных тестовыми данными, если она пуста
                await DbInitializer.InitializeAsync(serviceProvider, logger);
                
                logger.LogInformation("База данных успешно инициализирована.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Произошла ошибка при инициализации базы данных.");
                throw;
            }
        }
    }
} 