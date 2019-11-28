using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class integrationSetting : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TenantID",
                table: "IntegrationSettings",
                newName: "TenantId");

            migrationBuilder.AddColumn<string>(
                name: "ResourceId",
                table: "IntegrationSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ResourceUrl",
                table: "IntegrationSettings",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResourceId",
                table: "IntegrationSettings");

            migrationBuilder.DropColumn(
                name: "ResourceUrl",
                table: "IntegrationSettings");

            migrationBuilder.RenameColumn(
                name: "TenantId",
                table: "IntegrationSettings",
                newName: "TenantID");
        }
    }
}
