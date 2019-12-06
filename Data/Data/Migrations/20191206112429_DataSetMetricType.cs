using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class DataSetMetricType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MetricType",
                table: "Datasets",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MetricType",
                table: "Datasets");
        }
    }
}
