using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ChangeDashboardTypeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DashboardName",
                table: "DashboardTypes");

            migrationBuilder.AddColumn<int>(
                name: "DashboardTypeValue",
                table: "DashboardTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DashboardTypeValue",
                table: "DashboardSettings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DashboardTypeValue",
                table: "DashboardTypes");

            migrationBuilder.DropColumn(
                name: "DashboardTypeValue",
                table: "DashboardSettings");

            migrationBuilder.AddColumn<int>(
                name: "DashboardName",
                table: "DashboardTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
