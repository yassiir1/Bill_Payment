using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bill_payment.Migrations
{
    /// <inheritdoc />
    public partial class LastModifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SPocEmail",
                table: "Partner",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_FavouritePayments_UserId",
                table: "FavouritePayments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouritePayments_Users_UserId",
                table: "FavouritePayments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouritePayments_Users_UserId",
                table: "FavouritePayments");

            migrationBuilder.DropIndex(
                name: "IX_FavouritePayments_UserId",
                table: "FavouritePayments");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "SPocEmail",
                table: "Partner");
        }
    }
}
