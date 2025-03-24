using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bill_payment.Migrations
{
    /// <inheritdoc />
    public partial class AddPaymentNameCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentName",
                table: "FavouritePayments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentName",
                table: "FavouritePayments");
        }
    }
}
