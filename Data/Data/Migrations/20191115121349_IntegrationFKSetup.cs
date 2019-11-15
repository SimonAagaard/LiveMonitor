using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class IntegrationFKSetup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TenantID",
                table: "IntegrationSettings",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientSecret",
                table: "IntegrationSettings",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "IntegrationSettings",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Integrations",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntegrationSettings_IntegrationId",
                table: "IntegrationSettings",
                column: "IntegrationId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Integrations_UserId",
                table: "Integrations",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Integrations_AspNetUsers_UserId",
                table: "Integrations",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntegrationSettings_Integrations_IntegrationId",
                table: "IntegrationSettings",
                column: "IntegrationId",
                principalTable: "Integrations",
                principalColumn: "IntegrationId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Integrations_AspNetUsers_UserId",
                table: "Integrations");

            migrationBuilder.DropForeignKey(
                name: "FK_IntegrationSettings_Integrations_IntegrationId",
                table: "IntegrationSettings");

            migrationBuilder.DropIndex(
                name: "IX_IntegrationSettings_IntegrationId",
                table: "IntegrationSettings");

            migrationBuilder.DropIndex(
                name: "IX_Integrations_UserId",
                table: "Integrations");

            migrationBuilder.AlterColumn<string>(
                name: "TenantID",
                table: "IntegrationSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ClientSecret",
                table: "IntegrationSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "IntegrationSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Integrations",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
