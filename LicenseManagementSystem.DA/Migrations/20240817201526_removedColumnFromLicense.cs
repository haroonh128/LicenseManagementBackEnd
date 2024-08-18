using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenseManagementSystem.DA.Migrations
{
    /// <inheritdoc />
    public partial class removedColumnFromLicense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActivated",
                table: "licenses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActivated",
                table: "licenses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
