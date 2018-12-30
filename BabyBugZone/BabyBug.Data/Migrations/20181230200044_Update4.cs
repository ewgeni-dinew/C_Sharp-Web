using Microsoft.EntityFrameworkCore.Migrations;

namespace BabyBug.Data.Migrations
{
    public partial class Update4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessorySpecifications_Accessories_AccessoryId",
                table: "AccessorySpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_GarmentSpecifications_Garments_GarmentId",
                table: "GarmentSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderAccessories_Accessories_AccessoryId",
                table: "OrderAccessories");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderGarments_Garments_GarmentId",
                table: "OrderGarments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderShoes_Shoes_ShoeId",
                table: "OrderShoes");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoeSpecifications_Shoes_ShoeId",
                table: "ShoeSpecifications");

            migrationBuilder.DropIndex(
                name: "IX_OrderShoes_ShoeId",
                table: "OrderShoes");

            migrationBuilder.DropIndex(
                name: "IX_OrderGarments_GarmentId",
                table: "OrderGarments");

            migrationBuilder.DropIndex(
                name: "IX_OrderAccessories_AccessoryId",
                table: "OrderAccessories");

            migrationBuilder.DropColumn(
                name: "ShoeId",
                table: "OrderShoes");

            migrationBuilder.DropColumn(
                name: "GarmentId",
                table: "OrderGarments");

            migrationBuilder.DropColumn(
                name: "AccessoryId",
                table: "OrderAccessories");

            migrationBuilder.RenameColumn(
                name: "ShoeId",
                table: "ShoeSpecifications",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoeSpecifications_ShoeId",
                table: "ShoeSpecifications",
                newName: "IX_ShoeSpecifications_ProductId");

            migrationBuilder.RenameColumn(
                name: "GarmentId",
                table: "GarmentSpecifications",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_GarmentSpecifications_GarmentId",
                table: "GarmentSpecifications",
                newName: "IX_GarmentSpecifications_ProductId");

            migrationBuilder.RenameColumn(
                name: "AccessoryId",
                table: "AccessorySpecifications",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessorySpecifications_AccessoryId",
                table: "AccessorySpecifications",
                newName: "IX_AccessorySpecifications_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessorySpecifications_Accessories_ProductId",
                table: "AccessorySpecifications",
                column: "ProductId",
                principalTable: "Accessories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentSpecifications_Garments_ProductId",
                table: "GarmentSpecifications",
                column: "ProductId",
                principalTable: "Garments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderAccessories_Accessories_ProductId",
                table: "OrderAccessories",
                column: "ProductId",
                principalTable: "Accessories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderGarments_Garments_ProductId",
                table: "OrderGarments",
                column: "ProductId",
                principalTable: "Garments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderShoes_Shoes_ProductId",
                table: "OrderShoes",
                column: "ProductId",
                principalTable: "Shoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoeSpecifications_Shoes_ProductId",
                table: "ShoeSpecifications",
                column: "ProductId",
                principalTable: "Shoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessorySpecifications_Accessories_ProductId",
                table: "AccessorySpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_GarmentSpecifications_Garments_ProductId",
                table: "GarmentSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderAccessories_Accessories_ProductId",
                table: "OrderAccessories");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderGarments_Garments_ProductId",
                table: "OrderGarments");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderShoes_Shoes_ProductId",
                table: "OrderShoes");

            migrationBuilder.DropForeignKey(
                name: "FK_ShoeSpecifications_Shoes_ProductId",
                table: "ShoeSpecifications");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "ShoeSpecifications",
                newName: "ShoeId");

            migrationBuilder.RenameIndex(
                name: "IX_ShoeSpecifications_ProductId",
                table: "ShoeSpecifications",
                newName: "IX_ShoeSpecifications_ShoeId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "GarmentSpecifications",
                newName: "GarmentId");

            migrationBuilder.RenameIndex(
                name: "IX_GarmentSpecifications_ProductId",
                table: "GarmentSpecifications",
                newName: "IX_GarmentSpecifications_GarmentId");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "AccessorySpecifications",
                newName: "AccessoryId");

            migrationBuilder.RenameIndex(
                name: "IX_AccessorySpecifications_ProductId",
                table: "AccessorySpecifications",
                newName: "IX_AccessorySpecifications_AccessoryId");

            migrationBuilder.AddColumn<int>(
                name: "ShoeId",
                table: "OrderShoes",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GarmentId",
                table: "OrderGarments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AccessoryId",
                table: "OrderAccessories",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderShoes_ShoeId",
                table: "OrderShoes",
                column: "ShoeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderGarments_GarmentId",
                table: "OrderGarments",
                column: "GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAccessories_AccessoryId",
                table: "OrderAccessories",
                column: "AccessoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessorySpecifications_Accessories_AccessoryId",
                table: "AccessorySpecifications",
                column: "AccessoryId",
                principalTable: "Accessories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentSpecifications_Garments_GarmentId",
                table: "GarmentSpecifications",
                column: "GarmentId",
                principalTable: "Garments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderAccessories_Accessories_AccessoryId",
                table: "OrderAccessories",
                column: "AccessoryId",
                principalTable: "Accessories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderGarments_Garments_GarmentId",
                table: "OrderGarments",
                column: "GarmentId",
                principalTable: "Garments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderShoes_Shoes_ShoeId",
                table: "OrderShoes",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ShoeSpecifications_Shoes_ShoeId",
                table: "ShoeSpecifications",
                column: "ShoeId",
                principalTable: "Shoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
