using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashManager.Banking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TransactionProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "State",
                table: "Transactions");
        }
    }
}
