using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BabyBug.Data.Migrations
{
    public partial class AddedShoesAccessoriesToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentSpecifications_GarmentSizes_GarmentSizeId",
                table: "GarmentSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderGarments_Garments_GarmentId",
                table: "OrderGarments");

            migrationBuilder.DropTable(
                name: "GarmentSizes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderGarments",
                table: "OrderGarments");

            migrationBuilder.RenameColumn(
                name: "GarmentSizeId",
                table: "GarmentSpecifications",
                newName: "ProductSizeId");

            migrationBuilder.AlterColumn<int>(
                name: "GarmentId",
                table: "OrderGarments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "OrderGarments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderGarments",
                table: "OrderGarments",
                columns: new[] { "ProductId", "OrderId" });

            migrationBuilder.CreateTable(
                name: "AccessoryCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessoryCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductSizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShoeCategories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoeCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Accessories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accessories_AccessoryCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "AccessoryCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shoes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    IsAvailable = table.Column<bool>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    ImageId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shoes_ShoeCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ShoeCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AccessorySpecifications",
                columns: table => new
                {
                    AccessoryId = table.Column<int>(nullable: false),
                    ProductSizeId = table.Column<int>(nullable: false),
                    Quantity = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessorySpecifications", x => new { x.ProductSizeId, x.AccessoryId });
                    table.ForeignKey(
                        name: "FK_AccessorySpecifications_Accessories_AccessoryId",
                        column: x => x.AccessoryId,
                        principalTable: "Accessories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AccessorySpecifications_ProductSizes_ProductSizeId",
                        column: x => x.ProductSizeId,
                        principalTable: "ProductSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderAccessories",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    AccessoryId = table.Column<int>(nullable: true),
                    Quantity = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Size = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAccessories", x => new { x.ProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderAccessories_Accessories_AccessoryId",
                        column: x => x.AccessoryId,
                        principalTable: "Accessories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderAccessories_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderShoes",
                columns: table => new
                {
                    OrderId = table.Column<int>(nullable: false),
                    ProductId = table.Column<int>(nullable: false),
                    ShoeId = table.Column<int>(nullable: true),
                    Quantity = table.Column<long>(nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Size = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderShoes", x => new { x.ProductId, x.OrderId });
                    table.ForeignKey(
                        name: "FK_OrderShoes_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderShoes_Shoes_ShoeId",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShoeSpecifications",
                columns: table => new
                {
                    ShoeId = table.Column<int>(nullable: false),
                    ProductSizeId = table.Column<int>(nullable: false),
                    Quantity = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoeSpecifications", x => new { x.ProductSizeId, x.ShoeId });
                    table.ForeignKey(
                        name: "FK_ShoeSpecifications_ProductSizes_ProductSizeId",
                        column: x => x.ProductSizeId,
                        principalTable: "ProductSizes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShoeSpecifications_Shoes_ShoeId",
                        column: x => x.ShoeId,
                        principalTable: "Shoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderGarments_GarmentId",
                table: "OrderGarments",
                column: "GarmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Accessories_CategoryId",
                table: "Accessories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessorySpecifications_AccessoryId",
                table: "AccessorySpecifications",
                column: "AccessoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAccessories_AccessoryId",
                table: "OrderAccessories",
                column: "AccessoryId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAccessories_OrderId",
                table: "OrderAccessories",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderShoes_OrderId",
                table: "OrderShoes",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderShoes_ShoeId",
                table: "OrderShoes",
                column: "ShoeId");

            migrationBuilder.CreateIndex(
                name: "IX_Shoes_CategoryId",
                table: "Shoes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoeSpecifications_ShoeId",
                table: "ShoeSpecifications",
                column: "ShoeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentSpecifications_ProductSizes_ProductSizeId",
                table: "GarmentSpecifications",
                column: "ProductSizeId",
                principalTable: "ProductSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderGarments_Garments_GarmentId",
                table: "OrderGarments",
                column: "GarmentId",
                principalTable: "Garments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GarmentSpecifications_ProductSizes_ProductSizeId",
                table: "GarmentSpecifications");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderGarments_Garments_GarmentId",
                table: "OrderGarments");

            migrationBuilder.DropTable(
                name: "AccessorySpecifications");

            migrationBuilder.DropTable(
                name: "OrderAccessories");

            migrationBuilder.DropTable(
                name: "OrderShoes");

            migrationBuilder.DropTable(
                name: "ShoeSpecifications");

            migrationBuilder.DropTable(
                name: "Accessories");

            migrationBuilder.DropTable(
                name: "ProductSizes");

            migrationBuilder.DropTable(
                name: "Shoes");

            migrationBuilder.DropTable(
                name: "AccessoryCategories");

            migrationBuilder.DropTable(
                name: "ShoeCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderGarments",
                table: "OrderGarments");

            migrationBuilder.DropIndex(
                name: "IX_OrderGarments_GarmentId",
                table: "OrderGarments");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "OrderGarments");

            migrationBuilder.RenameColumn(
                name: "ProductSizeId",
                table: "GarmentSpecifications",
                newName: "GarmentSizeId");

            migrationBuilder.AlterColumn<int>(
                name: "GarmentId",
                table: "OrderGarments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderGarments",
                table: "OrderGarments",
                columns: new[] { "GarmentId", "OrderId" });

            migrationBuilder.CreateTable(
                name: "GarmentSizes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GarmentSizes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_GarmentSpecifications_GarmentSizes_GarmentSizeId",
                table: "GarmentSpecifications",
                column: "GarmentSizeId",
                principalTable: "GarmentSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderGarments_Garments_GarmentId",
                table: "OrderGarments",
                column: "GarmentId",
                principalTable: "Garments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
