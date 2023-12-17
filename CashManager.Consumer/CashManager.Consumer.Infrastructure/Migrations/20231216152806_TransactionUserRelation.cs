using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CashManager.Consumer.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class TransactionUserRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Articles_ArticleId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_ShoppingSession_ShoppingSessionId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingSession_Users_UserId",
                table: "ShoppingSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingSession",
                table: "ShoppingSession");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.RenameTable(
                name: "ShoppingSession",
                newName: "ShoppingSessions");

            migrationBuilder.RenameTable(
                name: "CartItem",
                newName: "CartItems");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingSession_UserId",
                table: "ShoppingSessions",
                newName: "IX_ShoppingSessions_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_ShoppingSessionId",
                table: "CartItems",
                newName: "IX_CartItems_ShoppingSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_ArticleId",
                table: "CartItems",
                newName: "IX_CartItems_ArticleId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingSessions",
                table: "ShoppingSessions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Articles_ArticleId",
                table: "CartItems",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_ShoppingSessions_ShoppingSessionId",
                table: "CartItems",
                column: "ShoppingSessionId",
                principalTable: "ShoppingSessions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingSessions_Users_UserId",
                table: "ShoppingSessions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Articles_ArticleId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_ShoppingSessions_ShoppingSessionId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingSessions_Users_UserId",
                table: "ShoppingSessions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Users_UserId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_UserId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ShoppingSessions",
                table: "ShoppingSessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transactions");

            migrationBuilder.RenameTable(
                name: "ShoppingSessions",
                newName: "ShoppingSession");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "CartItem");

            migrationBuilder.RenameIndex(
                name: "IX_ShoppingSessions_UserId",
                table: "ShoppingSession",
                newName: "IX_ShoppingSession_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_ShoppingSessionId",
                table: "CartItem",
                newName: "IX_CartItem_ShoppingSessionId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_ArticleId",
                table: "CartItem",
                newName: "IX_CartItem_ArticleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ShoppingSession",
                table: "ShoppingSession",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Articles_ArticleId",
                table: "CartItem",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_ShoppingSession_ShoppingSessionId",
                table: "CartItem",
                column: "ShoppingSessionId",
                principalTable: "ShoppingSession",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingSession_Users_UserId",
                table: "ShoppingSession",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
