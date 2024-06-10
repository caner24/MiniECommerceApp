using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniECommerceApp.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class stripe_user_id_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StripeUserId",
                table: "AspNetUsers",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StripeUserId",
                table: "AspNetUsers");
        }
    }
}
