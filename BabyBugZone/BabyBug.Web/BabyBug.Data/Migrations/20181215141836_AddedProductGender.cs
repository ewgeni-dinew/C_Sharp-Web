using Microsoft.EntityFrameworkCore.Migrations;

namespace BabyBug.Data.Migrations
{
    public partial class AddedProductGender : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Garments",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Garments");
        }
    }
}
