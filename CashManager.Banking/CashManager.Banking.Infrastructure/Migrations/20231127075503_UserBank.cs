using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashManager.Banking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserBank : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bank",
                table: "Accounts");

            migrationBuilder.AddColumn<int>(
                name: "Bank",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bank",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "Bank",
                table: "Accounts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
