using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Expositions",
                columns: new[] { "ExpositionId", "Description", "ExpositionName" },
                values: new object[,]
                {
                    { 1, "Коллекция древесных растений", "Дендрология" },
                    { 2, "Коллекция растений местной флоры", "Флора" },
                    { 3, "Коллекция декоративных цветочных растений", "Цветоводство" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Expositions",
                keyColumn: "ExpositionId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Expositions",
                keyColumn: "ExpositionId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Expositions",
                keyColumn: "ExpositionId",
                keyValue: 3);
        }
    }
}
