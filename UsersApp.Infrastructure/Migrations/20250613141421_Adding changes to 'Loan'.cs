using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsersApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingchangestoLoan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BookId",
                table: "Loans",
                newName: "ISBN");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Loans",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ISBN",
                table: "Loans",
                newName: "BookId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Loans",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
