using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DashboardGroupList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DashboardGroups_AspNetUsers_MonitorUserId",
                table: "DashboardGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_DashboardGroups_Dashboards_UserId",
                table: "DashboardGroups");

            migrationBuilder.DropIndex(
                name: "IX_DashboardGroups_MonitorUserId",
                table: "DashboardGroups");

            migrationBuilder.DropColumn(
                name: "DashboardId",
                table: "DashboardGroups");

            migrationBuilder.DropColumn(
                name: "MonitorUserId",
                table: "DashboardGroups");

            migrationBuilder.AddColumn<Guid>(
                name: "DashboardGroupId",
                table: "Dashboards",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_DashboardGroupId",
                table: "Dashboards",
                column: "DashboardGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_DashboardGroups_AspNetUsers_UserId",
                table: "DashboardGroups",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dashboards_DashboardGroups_DashboardGroupId",
                table: "Dashboards",
                column: "DashboardGroupId",
                principalTable: "DashboardGroups",
                principalColumn: "DashboardGroupId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DashboardGroups_AspNetUsers_UserId",
                table: "DashboardGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_Dashboards_DashboardGroups_DashboardGroupId",
                table: "Dashboards");

            migrationBuilder.DropIndex(
                name: "IX_Dashboards_DashboardGroupId",
                table: "Dashboards");

            migrationBuilder.DropColumn(
                name: "DashboardGroupId",
                table: "Dashboards");

            migrationBuilder.AddColumn<Guid>(
                name: "DashboardId",
                table: "DashboardGroups",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "MonitorUserId",
                table: "DashboardGroups",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DashboardGroups_MonitorUserId",
                table: "DashboardGroups",
                column: "MonitorUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_DashboardGroups_AspNetUsers_MonitorUserId",
                table: "DashboardGroups",
                column: "MonitorUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DashboardGroups_Dashboards_UserId",
                table: "DashboardGroups",
                column: "UserId",
                principalTable: "Dashboards",
                principalColumn: "DashboardId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
