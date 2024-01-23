using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaApplication.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGuidsToUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Customers_CustomerId",
                table: "Customers",
                column: "CustomerId",
                unique: true,
                filter: "[CustomerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAdmins_ContentAdminId",
                table: "ContentAdmins",
                column: "ContentAdminId",
                unique: true,
                filter: "[ContentAdminId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Admins_AdminId",
                table: "Admins",
                column: "AdminId",
                unique: true,
                filter: "[AdminId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Customers_CustomerId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_ContentAdmins_ContentAdminId",
                table: "ContentAdmins");

            migrationBuilder.DropIndex(
                name: "IX_Admins_AdminId",
                table: "Admins");
        }
    }
}
