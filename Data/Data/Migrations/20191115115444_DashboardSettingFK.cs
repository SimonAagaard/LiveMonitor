using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DashboardSettingFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "YLabel",
                table: "DashboardSettings",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "XLabel",
                table: "DashboardSettings",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DashboardSettings_DashboardId",
                table: "DashboardSettings",
                column: "DashboardId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DashboardSettings_Dashboards_DashboardId",
                table: "DashboardSettings",
                column: "DashboardId",
                principalTable: "Dashboards",
                principalColumn: "DashboardId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DashboardSettings_Dashboards_DashboardId",
                table: "DashboardSettings");

            migrationBuilder.DropIndex(
                name: "IX_DashboardSettings_DashboardId",
                table: "DashboardSettings");

            migrationBuilder.AlterColumn<string>(
                name: "YLabel",
                table: "DashboardSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "XLabel",
                table: "DashboardSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 128);
        }
    }
}
