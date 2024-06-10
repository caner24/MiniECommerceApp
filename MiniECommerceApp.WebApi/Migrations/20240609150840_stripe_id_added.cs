using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniECommerceApp.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class stripe_id_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeProductId",
                table: "Product",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeProductId",
                table: "Product");
        }
    }
}
