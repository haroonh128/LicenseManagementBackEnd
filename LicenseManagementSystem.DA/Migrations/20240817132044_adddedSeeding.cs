using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LicenseManagementSystem.DA.Migrations
{
    /// <inheritdoc />
    public partial class adddedSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedDate", "Email", "FirstName", "IsActive", "IsDeleted", "LastName", "ModifiedDate", "Password", "PhoneNumber", "Role", "UserName" },
                values: new object[,]
                {
                    { 1L, null, "admin@example.com", "Admin", false, false, "User", null, "Admin@123", "", "admin", "admin" },
                    { 2L, null, "user@example.com", "Regular", false, false, "User", null, "User@123", "", "user", "user" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2L);
        }
    }
}
