using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class IntegrationSettings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aggregation",
                table: "IntegrationSettings",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interval",
                table: "IntegrationSettings",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "IntegrationSettings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MetricName",
                table: "IntegrationSettings",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinutesOffset",
                table: "IntegrationSettings",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aggregation",
                table: "IntegrationSettings");

            migrationBuilder.DropColumn(
                name: "Interval",
                table: "IntegrationSettings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "IntegrationSettings");

            migrationBuilder.DropColumn(
                name: "MetricName",
                table: "IntegrationSettings");

            migrationBuilder.DropColumn(
                name: "MinutesOffset",
                table: "IntegrationSettings");
        }
    }
}
