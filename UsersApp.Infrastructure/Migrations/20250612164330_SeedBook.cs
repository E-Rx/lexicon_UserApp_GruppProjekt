using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UsersApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "ISBN", "Author", "Condition", "Genre", "Status", "Title" },
                values: new object[,]
                {
                    { "9780062315007", "Ernest Hemingway", 1, 0, 0, "Den gamle och havet" },
                    { "9780140449136", "Fjodor Dostojevskij", 1, 1, 0, "Brott och straff" },
                    { "9780143128540", "George Orwell", 0, 3, 0, "1984" },
                    { "9780307277671", "Yuval Noah Harari", 0, 9, 0, "Sapiens: En kort historik över mänskligheten" },
                    { "9780451524935", "Jane Austen", 2, 4, 2, "Stolthet och fördom" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "ISBN",
                keyValue: "9780062315007");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "ISBN",
                keyValue: "9780140449136");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "ISBN",
                keyValue: "9780143128540");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "ISBN",
                keyValue: "9780307277671");

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "ISBN",
                keyValue: "9780451524935");
        }
    }
}
