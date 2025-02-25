using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Добавляем пользователей
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "PasswordHash", "Role", "FullName", "CreatedAt" },
                values: new object[] { 1, "admin", "AQAAAAIAAYagAAAAELPTqiN5a8UUjYT9e/7TRZyTzgB4xbUcNPEzMk9lR8BUlhxA+EBuD8ASqzWQBdkQxw==", "staff", "Администратор", DateTime.UtcNow });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "PasswordHash", "Role", "FullName", "CreatedAt" },
                values: new object[] { 2, "botanist", "AQAAAAIAAYagAAAAELPTqiN5a8UUjYT9e/7TRZyTzgB4xbUcNPEzMk9lR8BUlhxA+EBuD8ASqzWQBdkQxw==", "staff", "Иван Иванов", DateTime.UtcNow });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Username", "PasswordHash", "Role", "FullName", "CreatedAt" },
                values: new object[] { 3, "visitor", "AQAAAAIAAYagAAAAELPTqiN5a8UUjYT9e/7TRZyTzgB4xbUcNPEzMk9lR8BUlhxA+EBuD8ASqzWQBdkQxw==", "client", "Петр Петров", DateTime.UtcNow });

            // Добавляем семейства растений
            migrationBuilder.InsertData(
                table: "PlantFamilies",
                columns: new[] { "PlantFamilyId", "Name" },
                values: new object[] { 1, "Rosaceae" });

            migrationBuilder.InsertData(
                table: "PlantFamilies",
                columns: new[] { "PlantFamilyId", "Name" },
                values: new object[] { 2, "Pinaceae" });

            migrationBuilder.InsertData(
                table: "PlantFamilies",
                columns: new[] { "PlantFamilyId", "Name" },
                values: new object[] { 3, "Asteraceae" });

            migrationBuilder.InsertData(
                table: "PlantFamilies",
                columns: new[] { "PlantFamilyId", "Name" },
                values: new object[] { 4, "Fabaceae" });

            migrationBuilder.InsertData(
                table: "PlantFamilies",
                columns: new[] { "PlantFamilyId", "Name" },
                values: new object[] { 5, "Poaceae" });

            // Добавляем области через SQL, чтобы правильно настроить геометрию
            migrationBuilder.Sql(@"
                INSERT INTO ""Areas"" (""AreaId"", ""Name"", ""Description"", ""Latitude"", ""Longitude"", ""Boundary"")
                VALUES (1, 'Розарий', 'Коллекция роз', 55.7520, 37.6175, 
                    ST_GeomFromText('POLYGON((37.6170 55.7515, 37.6180 55.7515, 37.6180 55.7525, 37.6170 55.7525, 37.6170 55.7515))', 4326));
                
                INSERT INTO ""Areas"" (""AreaId"", ""Name"", ""Description"", ""Latitude"", ""Longitude"", ""Boundary"")
                VALUES (2, 'Хвойный участок', 'Коллекция хвойных растений', 55.7530, 37.6185, 
                    ST_GeomFromText('POLYGON((37.6180 55.7525, 37.6190 55.7525, 37.6190 55.7535, 37.6180 55.7535, 37.6180 55.7525))', 4326));
                
                INSERT INTO ""Areas"" (""AreaId"", ""Name"", ""Description"", ""Latitude"", ""Longitude"", ""Boundary"")
                VALUES (3, 'Луговой участок', 'Коллекция луговых растений', 55.7540, 37.6195, 
                    ST_GeomFromText('POLYGON((37.6190 55.7535, 37.6200 55.7535, 37.6200 55.7545, 37.6190 55.7545, 37.6190 55.7535))', 4326));
            ");

            // Обновляем внешние ключи для областей
            migrationBuilder.Sql(@"
                UPDATE ""Areas"" SET ""ExpositionId"" = (SELECT ""ExpositionId"" FROM ""Expositions"" WHERE ""ExpositionName"" = 'Цветоводство')
                WHERE ""AreaId"" = 1;
                
                UPDATE ""Areas"" SET ""ExpositionId"" = (SELECT ""ExpositionId"" FROM ""Expositions"" WHERE ""ExpositionName"" = 'Дендрология')
                WHERE ""AreaId"" = 2;
                
                UPDATE ""Areas"" SET ""ExpositionId"" = (SELECT ""ExpositionId"" FROM ""Expositions"" WHERE ""ExpositionName"" = 'Флора')
                WHERE ""AreaId"" = 3;
            ");

            // Добавляем растения через SQL, чтобы правильно настроить геометрию
            migrationBuilder.Sql(@"
                INSERT INTO ""Plants"" (""PlantId"", ""InventoryNumber"", ""PlantFamilyId"", ""AreaId"", ""ExpositionId"", ""Genus"", ""Species"", ""Cultivar"", 
                    ""Form"", ""YearPlanted"", ""Origin"", ""NaturalRange"", ""EcologyBiology"", ""Usage"", ""ConservationStatus"", 
                    ""Latitude"", ""Longitude"", ""Location"", ""Note"", ""CreatedById"")
                VALUES (1, 'R001', 1, 1, 
                    (SELECT ""ExpositionId"" FROM ""Expositions"" WHERE ""ExpositionName"" = 'Цветоводство'),
                    'Rosa', 'canina', 'Alba', 
                    '', 2020, 'Европа', 'Европа, Западная Азия', 'Светолюбивое растение, предпочитает плодородные почвы', 
                    'Декоративное, лекарственное', 'Не охраняется', 55.7518, 37.6173, 
                    ST_SetSRID(ST_MakePoint(37.6173, 55.7518), 4326), 'Хорошо развивается, обильно цветет', 2);
                
                INSERT INTO ""Plants"" (""PlantId"", ""InventoryNumber"", ""PlantFamilyId"", ""AreaId"", ""ExpositionId"", ""Genus"", ""Species"", 
                    ""Cultivar"", ""Form"", ""YearPlanted"", ""Origin"", ""NaturalRange"", ""EcologyBiology"", ""Usage"", ""ConservationStatus"", 
                    ""Latitude"", ""Longitude"", ""Location"", ""Note"", ""CreatedById"")
                VALUES (2, 'P001', 2, 2, 
                    (SELECT ""ExpositionId"" FROM ""Expositions"" WHERE ""ExpositionName"" = 'Дендрология'),
                    'Pinus', 'sylvestris', 
                    '', '', 2019, 'Россия', 'Евразия', 'Холодостойкое, светолюбивое растение', 
                    'Декоративное, древесина, смола', 'Не охраняется', 55.7528, 37.6183, 
                    ST_SetSRID(ST_MakePoint(37.6183, 55.7528), 4326), 'Хороший рост', 2);
                
                INSERT INTO ""Plants"" (""PlantId"", ""InventoryNumber"", ""PlantFamilyId"", ""AreaId"", ""ExpositionId"", ""Genus"", ""Species"", 
                    ""Cultivar"", ""Form"", ""YearPlanted"", ""Origin"", ""NaturalRange"", ""EcologyBiology"", ""Usage"", ""ConservationStatus"", 
                    ""Latitude"", ""Longitude"", ""Location"", ""Note"", ""CreatedById"")
                VALUES (3, 'A001', 3, 3, 
                    (SELECT ""ExpositionId"" FROM ""Expositions"" WHERE ""ExpositionName"" = 'Флора'),
                    'Aster', 'alpinus', 
                    '', '', 2021, 'Альпы', 'Горные регионы Европы', 'Альпийское растение, предпочитает хорошо дренированные почвы', 
                    'Декоративное', 'Не охраняется', 55.7538, 37.6193, 
                    ST_SetSRID(ST_MakePoint(37.6193, 55.7538), 4326), 'Хорошая адаптация', 2);
            ");

            // Добавляем фенологические наблюдения
            migrationBuilder.InsertData(
                table: "PhenologyRecords",
                columns: new[] { "PhenologyRecordId", "PlantId", "ObservationYear", "DateBudBurst", "DateBloom", "DateFruiting", "DateLeafFall", "Comment" },
                values: new object[] { 1, 1, 2023, new DateTime(2023, 4, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 6, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 8, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 10, 20, 0, 0, 0, DateTimeKind.Utc), "Нормальное развитие" });

            migrationBuilder.InsertData(
                table: "PhenologyRecords",
                columns: new[] { "PhenologyRecordId", "PlantId", "ObservationYear", "DateBudBurst", "Comment" },
                values: new object[] { 2, 2, 2023, new DateTime(2023, 4, 10, 0, 0, 0, DateTimeKind.Utc), "Раннее распускание почек" });

            migrationBuilder.InsertData(
                table: "PhenologyRecords",
                columns: new[] { "PhenologyRecordId", "PlantId", "ObservationYear", "DateBudBurst", "DateBloom", "DateFruiting", "Comment" },
                values: new object[] { 3, 3, 2023, new DateTime(2023, 4, 20, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 6, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2023, 8, 25, 0, 0, 0, DateTimeKind.Utc), "Позднее цветение из-за холодной весны" });

            // Добавляем биометрические наблюдения
            migrationBuilder.InsertData(
                table: "BiometryRecords",
                columns: new[] { "BiometryRecordId", "PlantId", "MeasureDate", "HeightCm", "FlowerDiameter", "BudsCount", "Comment" },
                values: new object[] { 1, 1, new DateTime(2023, 7, 15, 0, 0, 0, DateTimeKind.Utc), 120, 8.5, 25, "Хорошее состояние" });

            migrationBuilder.InsertData(
                table: "BiometryRecords",
                columns: new[] { "BiometryRecordId", "PlantId", "MeasureDate", "HeightCm", "Comment" },
                values: new object[] { 2, 2, new DateTime(2023, 7, 20, 0, 0, 0, DateTimeKind.Utc), 350, "Активный рост" });

            migrationBuilder.InsertData(
                table: "BiometryRecords",
                columns: new[] { "BiometryRecordId", "PlantId", "MeasureDate", "HeightCm", "FlowerDiameter", "BudsCount", "Comment" },
                values: new object[] { 3, 3, new DateTime(2023, 7, 25, 0, 0, 0, DateTimeKind.Utc), 45, 3.2, 15, "Нормальное развитие" });

            // Добавляем статусы растений
            migrationBuilder.InsertData(
                table: "PlantStatuses",
                columns: new[] { "PlantStatusId", "PlantId", "Status", "StatusDate", "Comment", "UpdatedById" },
                values: new object[] { 1, 1, "Живое", DateTime.UtcNow.AddDays(-30), "Хорошее состояние", 2 });

            migrationBuilder.InsertData(
                table: "PlantStatuses",
                columns: new[] { "PlantStatusId", "PlantId", "Status", "StatusDate", "Comment", "UpdatedById" },
                values: new object[] { 2, 2, "Живое", DateTime.UtcNow.AddDays(-25), "Хорошее состояние", 2 });

            migrationBuilder.InsertData(
                table: "PlantStatuses",
                columns: new[] { "PlantStatusId", "PlantId", "Status", "StatusDate", "Comment", "UpdatedById" },
                values: new object[] { 3, 3, "Живое", DateTime.UtcNow.AddDays(-20), "Хорошее состояние", 2 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Удаляем данные в обратном порядке
            migrationBuilder.DeleteData(table: "PlantStatuses", keyColumn: "PlantStatusId", keyValue: 1);
            migrationBuilder.DeleteData(table: "PlantStatuses", keyColumn: "PlantStatusId", keyValue: 2);
            migrationBuilder.DeleteData(table: "PlantStatuses", keyColumn: "PlantStatusId", keyValue: 3);

            migrationBuilder.DeleteData(table: "BiometryRecords", keyColumn: "BiometryRecordId", keyValue: 1);
            migrationBuilder.DeleteData(table: "BiometryRecords", keyColumn: "BiometryRecordId", keyValue: 2);
            migrationBuilder.DeleteData(table: "BiometryRecords", keyColumn: "BiometryRecordId", keyValue: 3);

            migrationBuilder.DeleteData(table: "PhenologyRecords", keyColumn: "PhenologyRecordId", keyValue: 1);
            migrationBuilder.DeleteData(table: "PhenologyRecords", keyColumn: "PhenologyRecordId", keyValue: 2);
            migrationBuilder.DeleteData(table: "PhenologyRecords", keyColumn: "PhenologyRecordId", keyValue: 3);

            migrationBuilder.DeleteData(table: "Plants", keyColumn: "PlantId", keyValue: 1);
            migrationBuilder.DeleteData(table: "Plants", keyColumn: "PlantId", keyValue: 2);
            migrationBuilder.DeleteData(table: "Plants", keyColumn: "PlantId", keyValue: 3);

            migrationBuilder.DeleteData(table: "Areas", keyColumn: "AreaId", keyValue: 1);
            migrationBuilder.DeleteData(table: "Areas", keyColumn: "AreaId", keyValue: 2);
            migrationBuilder.DeleteData(table: "Areas", keyColumn: "AreaId", keyValue: 3);

            // Примечание: экспозиции удаляются в миграции v1

            migrationBuilder.DeleteData(table: "PlantFamilies", keyColumn: "PlantFamilyId", keyValue: 1);
            migrationBuilder.DeleteData(table: "PlantFamilies", keyColumn: "PlantFamilyId", keyValue: 2);
            migrationBuilder.DeleteData(table: "PlantFamilies", keyColumn: "PlantFamilyId", keyValue: 3);
            migrationBuilder.DeleteData(table: "PlantFamilies", keyColumn: "PlantFamilyId", keyValue: 4);
            migrationBuilder.DeleteData(table: "PlantFamilies", keyColumn: "PlantFamilyId", keyValue: 5);

            migrationBuilder.DeleteData(table: "Users", keyColumn: "UserId", keyValue: 1);
            migrationBuilder.DeleteData(table: "Users", keyColumn: "UserId", keyValue: 2);
            migrationBuilder.DeleteData(table: "Users", keyColumn: "UserId", keyValue: 3);
        }
    }
}
