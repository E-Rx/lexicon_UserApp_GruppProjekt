using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Addingseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "ISBN",
                keyValue: "9780140449136",
                column: "Status",
                value: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "ISBN",
                keyValue: "9780140449136",
                column: "Status",
                value: 0);
        }
    }
}
