using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashManager.Consumer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ShoppingSessionRemoveDuplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdArticle",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "IdShoppingSession",
                table: "CartItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdArticle",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "IdShoppingSession",
                table: "CartItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
