using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniECommerceApp.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class mig_5_some_entity_configured : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "ProductDetail");

            migrationBuilder.AddColumn<string>(
                name: "ProductDetailText",
                table: "ProductDetail",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductDetailText",
                table: "ProductDetail");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Product");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "ProductDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
