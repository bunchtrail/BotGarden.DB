using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Infrastructure.Data
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            // Получаем путь к текущей директории
            var basePath = Directory.GetCurrentDirectory();
            
            // Создаем конфигурацию из appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development"}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
            
            // Получаем строку подключения из конфигурации
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            
            // Создаем опции для контекста базы данных
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseNpgsql(connectionString, 
                o => o.UseNetTopologySuite() // Включаем поддержку PostGIS
                      .MigrationsAssembly("Infrastructure")); // Указываем, где хранятся миграции
            
            return new AppDbContext(optionsBuilder.Options);
        }
    }
} 