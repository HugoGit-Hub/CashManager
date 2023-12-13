using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashManager.Consumer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingSessionState : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "State",
                table: "ShoppingSession",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "State",
                table: "ShoppingSession");
        }
    }
}
