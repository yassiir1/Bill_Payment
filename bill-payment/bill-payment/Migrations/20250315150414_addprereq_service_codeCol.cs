using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bill_payment.Migrations
{
    /// <inheritdoc />
    public partial class addprereq_service_codeCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "prereq_service_code",
                table: "FavouritePayments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "prereq_service_code",
                table: "FavouritePayments");
        }
    }
}
