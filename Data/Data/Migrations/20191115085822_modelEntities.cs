using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class modelEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateDeleted",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Dashboards",
                columns: table => new
                {
                    DashboardId = table.Column<Guid>(nullable: false),
                    DashboardName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    DashboardSettingId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false),
                    DateDeleted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboards", x => x.DashboardId);
                });

            migrationBuilder.CreateTable(
                name: "DashboardSettings",
                columns: table => new
                {
                    DashboardSettingId = table.Column<Guid>(nullable: false),
                    DashboardId = table.Column<Guid>(nullable: false),
                    DashboardTypeId = table.Column<Guid>(nullable: false),
                    RefreshRate = table.Column<int>(nullable: false),
                    XLabel = table.Column<string>(nullable: true),
                    YLabel = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardSettings", x => x.DashboardSettingId);
                });

            migrationBuilder.CreateTable(
                name: "DashboardTypes",
                columns: table => new
                {
                    DashboardTypeId = table.Column<Guid>(nullable: false),
                    DashboardName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardTypes", x => x.DashboardTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Datasets",
                columns: table => new
                {
                    DataSetId = table.Column<Guid>(nullable: false),
                    XValue = table.Column<DateTime>(nullable: false),
                    YValue = table.Column<int>(nullable: false),
                    IntegrationSettingId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datasets", x => x.DataSetId);
                });

            migrationBuilder.CreateTable(
                name: "Integrations",
                columns: table => new
                {
                    IntegrationId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    IntegrationSettingId = table.Column<Guid>(nullable: false),
                    IntegrationName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Integrations", x => x.IntegrationId);
                });

            migrationBuilder.CreateTable(
                name: "IntegrationSettings",
                columns: table => new
                {
                    IntegrationSettingId = table.Column<Guid>(nullable: false),
                    IntegrationId = table.Column<Guid>(nullable: false),
                    ClientId = table.Column<string>(nullable: true),
                    ClientSecret = table.Column<string>(nullable: true),
                    TenantID = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IntegrationSettings", x => x.IntegrationSettingId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dashboards");

            migrationBuilder.DropTable(
                name: "DashboardSettings");

            migrationBuilder.DropTable(
                name: "DashboardTypes");

            migrationBuilder.DropTable(
                name: "Datasets");

            migrationBuilder.DropTable(
                name: "Integrations");

            migrationBuilder.DropTable(
                name: "IntegrationSettings");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
