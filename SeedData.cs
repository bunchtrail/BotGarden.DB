using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BotGarden.DB
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Начинаю заполнение базы данных тестовыми данными...");

            // Создаем сервисы
            var services = new ServiceCollection();
            
            // Добавляем логгер
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Information);
            });

            // Добавляем контекст базы данных
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(
                    "Host=localhost;Database=botgarden;Username=postgres;Password=postgres",
                    x => x.UseNetTopologySuite()));

            var serviceProvider = services.BuildServiceProvider();

            // Получаем логгер
            var logger = serviceProvider.GetRequiredService<ILogger<AppDbContext>>();

            try
            {
                // Вызываем метод инициализации данных
                await DbInitializer.InitializeAsync(serviceProvider, logger);
                Console.WriteLine("База данных успешно заполнена тестовыми данными.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при заполнении базы данных: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
            }
        }
    }
} 