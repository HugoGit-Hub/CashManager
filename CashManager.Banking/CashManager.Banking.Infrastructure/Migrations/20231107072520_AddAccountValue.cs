using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashManager.Banking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAccountValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Value",
                table: "Accounts",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Value",
                table: "Accounts");
        }
    }
}
