using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class CreateDataSetFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Datasets_IntegrationSettingId",
                table: "Datasets",
                column: "IntegrationSettingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Datasets_IntegrationSettings_IntegrationSettingId",
                table: "Datasets",
                column: "IntegrationSettingId",
                principalTable: "IntegrationSettings",
                principalColumn: "IntegrationSettingId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Datasets_IntegrationSettings_IntegrationSettingId",
                table: "Datasets");

            migrationBuilder.DropIndex(
                name: "IX_Datasets_IntegrationSettingId",
                table: "Datasets");
        }
    }
}
