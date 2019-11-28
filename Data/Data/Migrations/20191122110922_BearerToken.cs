using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class BearerToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BearerToken",
                columns: table => new
                {
                    BearerTokenId = table.Column<Guid>(nullable: false),
                    AccessToken = table.Column<string>(nullable: true),
                    IntegrationSettingId = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateExpired = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BearerToken", x => x.BearerTokenId);
                    table.ForeignKey(
                        name: "FK_BearerToken_IntegrationSettings_IntegrationSettingId",
                        column: x => x.IntegrationSettingId,
                        principalTable: "IntegrationSettings",
                        principalColumn: "IntegrationSettingId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BearerToken_IntegrationSettingId",
                table: "BearerToken",
                column: "IntegrationSettingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BearerToken");
        }
    }
}
