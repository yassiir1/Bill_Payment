using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace bill_payment.Migrations
{
    /// <inheritdoc />
    public partial class AddFavouritePayments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavouritePayments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    service_provider_code = table.Column<string>(type: "text", nullable: false),
                    service_code = table.Column<string>(type: "text", nullable: false),
                    user_account = table.Column<string>(type: "text", nullable: false),
                    package_code = table.Column<string>(type: "text", nullable: false),
                    bill_type = table.Column<int>(type: "integer", nullable: false),
                    last_paid_amount = table.Column<double>(type: "double precision", nullable: false),
                    is_bill = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavouritePayments", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FavouritePayments");
        }
    }
}
