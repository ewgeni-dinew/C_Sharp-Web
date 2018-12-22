using Microsoft.EntityFrameworkCore.Migrations;

namespace BabyBug.Data.Migrations
{
    public partial class AddImageId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "Garments",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageId",
                table: "GarmentCategories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "Garments");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "GarmentCategories");
        }
    }
}
