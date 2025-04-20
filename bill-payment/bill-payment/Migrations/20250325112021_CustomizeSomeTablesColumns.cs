using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bill_payment.Migrations
{
    /// <inheritdoc />
    public partial class CustomizeSomeTablesColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "PartnerId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Partner",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsGedieaPayEnabled",
                table: "Partner",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPartnerWalletEnabled",
                table: "Partner",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "SessionTimeInMins",
                table: "Partner",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Partner",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PartnerId",
                table: "Users",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FavouritePayments_user_account_service_code",
                table: "FavouritePayments",
                columns: new[] { "user_account", "service_code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CreditCards_token_id",
                table: "CreditCards",
                column: "token_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Partner_PartnerId",
                table: "Users",
                column: "PartnerId",
                principalTable: "Partner",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Partner_PartnerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_PartnerId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_FavouritePayments_user_account_service_code",
                table: "FavouritePayments");

            migrationBuilder.DropIndex(
                name: "IX_CreditCards_token_id",
                table: "CreditCards");

            migrationBuilder.DropColumn(
                name: "Age",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Partner");

            migrationBuilder.DropColumn(
                name: "IsGedieaPayEnabled",
                table: "Partner");

            migrationBuilder.DropColumn(
                name: "IsPartnerWalletEnabled",
                table: "Partner");

            migrationBuilder.DropColumn(
                name: "SessionTimeInMins",
                table: "Partner");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Partner");

            migrationBuilder.AlterColumn<Guid>(
                name: "PartnerId",
                table: "Users",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }
    }
}
