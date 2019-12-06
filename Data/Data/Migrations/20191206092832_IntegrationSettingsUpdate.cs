using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class IntegrationSettingsUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TenantId",
                table: "IntegrationSettings",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResourceUrl",
                table: "IntegrationSettings",
                maxLength: 510,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResourceId",
                table: "IntegrationSettings",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientSecret",
                table: "IntegrationSettings",
                maxLength: 32,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "IntegrationSettings",
                maxLength: 36,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

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
                defaultValue: true);

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

            migrationBuilder.AlterColumn<string>(
                name: "TenantId",
                table: "IntegrationSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 36,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResourceUrl",
                table: "IntegrationSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 510,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ResourceId",
                table: "IntegrationSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientSecret",
                table: "IntegrationSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 32,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ClientId",
                table: "IntegrationSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 36,
                oldNullable: true);
        }
    }
}
