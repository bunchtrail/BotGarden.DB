using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider, ILogger<AppDbContext> logger)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            try
            {
                // Применяем миграции
                await context.Database.MigrateAsync();

                // Если база данных уже содержит данные, не инициализируем её
                if (context.Users.Any())
                {
                    logger.LogInformation("База данных уже содержит данные. Инициализация не требуется.");
                    return;
                }

                // Инициализируем базу данных тестовыми данными
                await SeedDataAsync(context);
                logger.LogInformation("База данных успешно инициализирована тестовыми данными.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Произошла ошибка при инициализации базы данных.");
                throw;
            }
        }

        private static async Task SeedDataAsync(AppDbContext context)
        {
            // Создаем пользователей
            var users = new List<User>
            {
                new User
                {
                    Username = "admin",
                    PasswordHash = "AQAAAAIAAYagAAAAELPTqiN5a8UUjYT9e/7TRZyTzgB4xbUcNPEzMk9lR8BUlhxA+EBuD8ASqzWQBdkQxw==", // Password: Admin123!
                    Role = "staff",
                    FullName = "Администратор",
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    Username = "botanist",
                    PasswordHash = "AQAAAAIAAYagAAAAELPTqiN5a8UUjYT9e/7TRZyTzgB4xbUcNPEzMk9lR8BUlhxA+EBuD8ASqzWQBdkQxw==", // Password: Botanist123!
                    Role = "staff",
                    FullName = "Иван Иванов",
                    CreatedAt = DateTime.Now
                },
                new User
                {
                    Username = "visitor",
                    PasswordHash = "AQAAAAIAAYagAAAAELPTqiN5a8UUjYT9e/7TRZyTzgB4xbUcNPEzMk9lR8BUlhxA+EBuD8ASqzWQBdkQxw==", // Password: Visitor123!
                    Role = "client",
                    FullName = "Петр Петров",
                    CreatedAt = DateTime.Now
                }
            };
            await context.Users.AddRangeAsync(users);
            await context.SaveChangesAsync();

            // Создаем семейства растений
            var plantFamilies = new List<PlantFamily>
            {
                new PlantFamily { Name = "Rosaceae" }, // Розовые
                new PlantFamily { Name = "Pinaceae" }, // Сосновые
                new PlantFamily { Name = "Asteraceae" }, // Астровые
                new PlantFamily { Name = "Fabaceae" }, // Бобовые
                new PlantFamily { Name = "Poaceae" } // Злаковые
            };
            await context.PlantFamilies.AddRangeAsync(plantFamilies);
            await context.SaveChangesAsync();

            // Создаем экспозиции
            var expositions = new List<Exposition>
            {
                new Exposition { ExpositionName = "Дендрология", Description = "Коллекция древесных растений" },
                new Exposition { ExpositionName = "Цветоводство", Description = "Коллекция цветочных растений" },
                new Exposition { ExpositionName = "Флора", Description = "Коллекция растений местной флоры" }
            };
            await context.Expositions.AddRangeAsync(expositions);
            await context.SaveChangesAsync();

            // Создаем области
            var areas = new List<Area>
            {
                new Area
                {
                    Name = "Розарий",
                    Description = "Коллекция роз",
                    ExpositionId = expositions[1].ExpositionId,
                    Latitude = 55.7520,
                    Longitude = 37.6175,
                    Boundary = CreatePolygon(new[]
                    {
                        new Coordinate(37.6170, 55.7515),
                        new Coordinate(37.6180, 55.7515),
                        new Coordinate(37.6180, 55.7525),
                        new Coordinate(37.6170, 55.7525),
                        new Coordinate(37.6170, 55.7515)
                    })
                },
                new Area
                {
                    Name = "Хвойный участок",
                    Description = "Коллекция хвойных растений",
                    ExpositionId = expositions[0].ExpositionId,
                    Latitude = 55.7530,
                    Longitude = 37.6185,
                    Boundary = CreatePolygon(new[]
                    {
                        new Coordinate(37.6180, 55.7525),
                        new Coordinate(37.6190, 55.7525),
                        new Coordinate(37.6190, 55.7535),
                        new Coordinate(37.6180, 55.7535),
                        new Coordinate(37.6180, 55.7525)
                    })
                },
                new Area
                {
                    Name = "Луговой участок",
                    Description = "Коллекция луговых растений",
                    ExpositionId = expositions[2].ExpositionId,
                    Latitude = 55.7540,
                    Longitude = 37.6195,
                    Boundary = CreatePolygon(new[]
                    {
                        new Coordinate(37.6190, 55.7535),
                        new Coordinate(37.6200, 55.7535),
                        new Coordinate(37.6200, 55.7545),
                        new Coordinate(37.6190, 55.7545),
                        new Coordinate(37.6190, 55.7535)
                    })
                }
            };
            await context.Areas.AddRangeAsync(areas);
            await context.SaveChangesAsync();

            // Создаем растения
            var plants = new List<Plant>
            {
                new Plant
                {
                    InventoryNumber = "R001",
                    PlantFamilyId = plantFamilies[0].PlantFamilyId,
                    ExpositionId = expositions[1].ExpositionId,
                    AreaId = areas[0].AreaId,
                    Genus = "Rosa",
                    Species = "canina",
                    Cultivar = "Alba",
                    YearPlanted = 2020,
                    Origin = "Европа",
                    NaturalRange = "Европа, Западная Азия",
                    EcologyBiology = "Светолюбивое растение, предпочитает плодородные почвы",
                    Usage = "Декоративное, лекарственное",
                    ConservationStatus = "Не охраняется",
                    Latitude = 55.7518,
                    Longitude = 37.6173,
                    Location = new Point(37.6173, 55.7518) { SRID = 4326 },
                    Note = "Хорошо развивается, обильно цветет",
                    CreatedById = users[1].UserId
                },
                new Plant
                {
                    InventoryNumber = "P001",
                    PlantFamilyId = plantFamilies[1].PlantFamilyId,
                    ExpositionId = expositions[0].ExpositionId,
                    AreaId = areas[1].AreaId,
                    Genus = "Pinus",
                    Species = "sylvestris",
                    YearPlanted = 2015,
                    Origin = "Россия",
                    NaturalRange = "Евразия",
                    EcologyBiology = "Светолюбивое растение, неприхотливое к почвам",
                    Usage = "Декоративное, древесина, смола",
                    ConservationStatus = "Не охраняется",
                    Latitude = 55.7528,
                    Longitude = 37.6183,
                    Location = new Point(37.6183, 55.7528) { SRID = 4326 },
                    Note = "Хорошо развивается",
                    CreatedById = users[1].UserId
                },
                new Plant
                {
                    InventoryNumber = "A001",
                    PlantFamilyId = plantFamilies[2].PlantFamilyId,
                    ExpositionId = expositions[2].ExpositionId,
                    AreaId = areas[2].AreaId,
                    Genus = "Achillea",
                    Species = "millefolium",
                    YearPlanted = 2022,
                    Origin = "Россия",
                    NaturalRange = "Евразия, Северная Америка",
                    EcologyBiology = "Светолюбивое растение, неприхотливое к почвам",
                    Usage = "Декоративное, лекарственное",
                    ConservationStatus = "Не охраняется",
                    Latitude = 55.7538,
                    Longitude = 37.6193,
                    Location = new Point(37.6193, 55.7538) { SRID = 4326 },
                    Note = "Хорошо развивается, обильно цветет",
                    CreatedById = users[1].UserId
                }
            };
            await context.Plants.AddRangeAsync(plants);
            await context.SaveChangesAsync();

            // Создаем фенологические наблюдения
            var phenologyRecords = new List<PhenologyRecord>
            {
                new PhenologyRecord
                {
                    PlantId = plants[0].PlantId,
                    ObservationYear = 2023,
                    DateBudBurst = new DateTime(2023, 4, 15),
                    DateBloom = new DateTime(2023, 6, 1),
                    DateFruiting = new DateTime(2023, 8, 15),
                    DateLeafFall = new DateTime(2023, 10, 20),
                    Comment = "Нормальное развитие"
                },
                new PhenologyRecord
                {
                    PlantId = plants[1].PlantId,
                    ObservationYear = 2023,
                    DateBudBurst = new DateTime(2023, 4, 10),
                    Comment = "Раннее распускание почек"
                },
                new PhenologyRecord
                {
                    PlantId = plants[2].PlantId,
                    ObservationYear = 2023,
                    DateBudBurst = new DateTime(2023, 4, 20),
                    DateBloom = new DateTime(2023, 6, 15),
                    DateFruiting = new DateTime(2023, 8, 25),
                    Comment = "Позднее цветение из-за холодной весны"
                }
            };
            await context.PhenologyRecords.AddRangeAsync(phenologyRecords);
            await context.SaveChangesAsync();

            // Создаем биометрические наблюдения
            var biometryRecords = new List<BiometryRecord>
            {
                new BiometryRecord
                {
                    PlantId = plants[0].PlantId,
                    MeasureDate = new DateTime(2023, 7, 15),
                    HeightCm = 120,
                    FlowerDiameter = 8.5,
                    BudsCount = 25,
                    Comment = "Хорошее состояние"
                },
                new BiometryRecord
                {
                    PlantId = plants[1].PlantId,
                    MeasureDate = new DateTime(2023, 7, 20),
                    HeightCm = 350,
                    Comment = "Активный рост"
                },
                new BiometryRecord
                {
                    PlantId = plants[2].PlantId,
                    MeasureDate = new DateTime(2023, 7, 25),
                    HeightCm = 45,
                    FlowerDiameter = 3.2,
                    BudsCount = 15,
                    Comment = "Нормальное развитие"
                }
            };
            await context.BiometryRecords.AddRangeAsync(biometryRecords);
            await context.SaveChangesAsync();

            // Создаем статусы растений
            var plantStatuses = new List<PlantStatus>
            {
                new PlantStatus
                {
                    PlantId = plants[0].PlantId,
                    Status = "Живое",
                    StatusDate = DateTime.Now.AddDays(-30),
                    Comment = "Хорошее состояние",
                    UpdatedById = users[1].UserId
                },
                new PlantStatus
                {
                    PlantId = plants[1].PlantId,
                    Status = "Живое",
                    StatusDate = DateTime.Now.AddDays(-25),
                    Comment = "Хорошее состояние",
                    UpdatedById = users[1].UserId
                },
                new PlantStatus
                {
                    PlantId = plants[2].PlantId,
                    Status = "Живое",
                    StatusDate = DateTime.Now.AddDays(-20),
                    Comment = "Хорошее состояние",
                    UpdatedById = users[1].UserId
                }
            };
            await context.PlantStatuses.AddRangeAsync(plantStatuses);
            await context.SaveChangesAsync();
        }

        private static Polygon CreatePolygon(Coordinate[] coordinates)
        {
            var geometryFactory = NetTopologySuite.NtsGeometryServices.Instance.CreateGeometryFactory(4326);
            var ring = geometryFactory.CreateLinearRing(coordinates);
            return geometryFactory.CreatePolygon(ring);
        }
    }
} 