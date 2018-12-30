using Microsoft.EntityFrameworkCore.Migrations;

namespace BabyBug.Data.Migrations
{
    public partial class Update5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryType",
                table: "ShoeCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryType",
                table: "GarmentCategories",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CategoryType",
                table: "AccessoryCategories",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "ShoeCategories");

            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "GarmentCategories");

            migrationBuilder.DropColumn(
                name: "CategoryType",
                table: "AccessoryCategories");
        }
    }
}
