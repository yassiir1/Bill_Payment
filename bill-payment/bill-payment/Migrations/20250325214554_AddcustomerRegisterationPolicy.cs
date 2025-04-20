using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace bill_payment.Migrations
{
    /// <inheritdoc />
    public partial class AddcustomerRegisterationPolicy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "customerRegisterationPolicy",
                table: "Partner",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "customerRegisterationPolicy",
                table: "Partner");
        }
    }
}
