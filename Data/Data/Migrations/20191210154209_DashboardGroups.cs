using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DashboardGroups : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DashboardGroups",
                columns: table => new
                {
                    DashboardGroupId = table.Column<Guid>(nullable: false),
                    DashboardId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    GroupRefreshRate = table.Column<int>(nullable: false),
                    DashboardGroupName = table.Column<string>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    DateDeleted = table.Column<DateTimeOffset>(nullable: true),
                    MonitorUserId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DashboardGroups", x => x.DashboardGroupId);
                    table.ForeignKey(
                        name: "FK_DashboardGroups_AspNetUsers_MonitorUserId",
                        column: x => x.MonitorUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DashboardGroups_Dashboards_UserId",
                        column: x => x.UserId,
                        principalTable: "Dashboards",
                        principalColumn: "DashboardId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DashboardGroups_MonitorUserId",
                table: "DashboardGroups",
                column: "MonitorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_DashboardGroups_UserId",
                table: "DashboardGroups",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DashboardGroups");
        }
    }
}
