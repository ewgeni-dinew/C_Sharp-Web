using Microsoft.EntityFrameworkCore.Migrations;

namespace BabyBug.Data.Migrations
{
    public partial class RemoeedTotalPriceOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Orders",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
