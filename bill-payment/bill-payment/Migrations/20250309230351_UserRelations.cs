using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bill_payment.Migrations
{
    /// <inheritdoc />
    public partial class UserRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GiedeaUser_id",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "session_id",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "skey",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "FavouritePayments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "CreditCards",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GiedeaUser_id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "session_id",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "skey",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "FavouritePayments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CreditCards");
        }
    }
}
