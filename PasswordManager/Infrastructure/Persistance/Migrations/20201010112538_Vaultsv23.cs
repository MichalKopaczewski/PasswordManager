using Microsoft.EntityFrameworkCore.Migrations;

namespace PasswordManager.infrastructure.persistance.migrations
{
    public partial class Vaultsv23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MasterSalt",
                table: "Vaults");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Entries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Entries");

            migrationBuilder.AddColumn<string>(
                name: "MasterSalt",
                table: "Vaults",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
