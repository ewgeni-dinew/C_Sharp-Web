﻿// <auto-generated />
using System;
using BabyBugZone.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BabyBug.Data.Migrations
{
    [DbContext(typeof(BabyBugDbContext))]
    [Migration("20181230173000_Update3")]
    partial class Update3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BabyBug.Data.Models.BabyBugUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName")
                        .IsRequired();

                    b.Property<string>("LastName")
                        .IsRequired();

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("BabyBug.Data.Models.BlogPage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Author");

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Header");

                    b.Property<string>("ImageId");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsDeleted");

                    b.HasKey("Id");

                    b.ToTable("BlogPages");
                });

            modelBuilder.Entity("BabyBug.Data.Models.Categories.AccessoryCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageId");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("AccessoryCategories");
                });

            modelBuilder.Entity("BabyBug.Data.Models.Categories.GarmentCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageId");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("GarmentCategories");
                });

            modelBuilder.Entity("BabyBug.Data.Models.Categories.ShoeCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ImageId");

                    b.Property<string>("ImageUrl");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("ShoeCategories");
                });

            modelBuilder.Entity("BabyBug.Data.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DeliveryDestination");

                    b.Property<int>("DeliveryType");

                    b.Property<DateTime>("MadeOn_Date");

                    b.Property<int>("PaymentType");

                    b.Property<int>("Status");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BabyBug.Data.Models.OrderProducts.OrderAccessories", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("OrderId");

                    b.Property<int?>("AccessoryId");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<long>("Quantity");

                    b.Property<string>("Size");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("AccessoryId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderAccessories");
                });

            modelBuilder.Entity("BabyBug.Data.Models.OrderProducts.OrderGarments", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("OrderId");

                    b.Property<int?>("GarmentId");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<long>("Quantity");

                    b.Property<string>("Size");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("GarmentId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderGarments");
                });

            modelBuilder.Entity("BabyBug.Data.Models.OrderProducts.OrderShoes", b =>
                {
                    b.Property<int>("ProductId");

                    b.Property<int>("OrderId");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.Property<long>("Quantity");

                    b.Property<int?>("ShoeId");

                    b.Property<string>("Size");

                    b.HasKey("ProductId", "OrderId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ShoeId");

                    b.ToTable("OrderShoes");
                });

            modelBuilder.Entity("BabyBug.Data.Models.ProductSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Value");

                    b.HasKey("Id");

                    b.ToTable("ProductSizes");
                });

            modelBuilder.Entity("BabyBug.Data.Models.ProductSpecifications.AccessorySpecification", b =>
                {
                    b.Property<int>("ProductSizeId");

                    b.Property<int>("AccessoryId");

                    b.Property<long>("Quantity");

                    b.HasKey("ProductSizeId", "AccessoryId");

                    b.HasIndex("AccessoryId");

                    b.ToTable("AccessorySpecifications");
                });

            modelBuilder.Entity("BabyBug.Data.Models.ProductSpecifications.GarmentSpecification", b =>
                {
                    b.Property<int>("ProductSizeId");

                    b.Property<int>("GarmentId");

                    b.Property<long>("Quantity");

                    b.HasKey("ProductSizeId", "GarmentId");

                    b.HasIndex("GarmentId");

                    b.ToTable("GarmentSpecifications");
                });

            modelBuilder.Entity("BabyBug.Data.Models.ProductSpecifications.ShoeSpecification", b =>
                {
                    b.Property<int>("ProductSizeId");

                    b.Property<int>("ShoeId");

                    b.Property<long>("Quantity");

                    b.HasKey("ProductSizeId", "ShoeId");

                    b.HasIndex("ShoeId");

                    b.ToTable("ShoeSpecifications");
                });

            modelBuilder.Entity("BabyBug.Data.Models.Products.Accessory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<string>("ImageId");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsAvailable");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Accessories");
                });

            modelBuilder.Entity("BabyBug.Data.Models.Products.Garment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<string>("ImageId");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsAvailable");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Garments");
                });

            modelBuilder.Entity("BabyBug.Data.Models.Products.Shoe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId");

                    b.Property<DateTime>("CreatedOn");

                    b.Property<string>("Description");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasConversion(new ValueConverter<string, string>(v => default(string), v => default(string), new ConverterMappingHints(size: 1)));

                    b.Property<string>("ImageId");

                    b.Property<string>("ImageUrl");

                    b.Property<bool>("IsAvailable");

                    b.Property<string>("Name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18, 2)");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Shoes");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("BabyBug.Data.Models.Order", b =>
                {
                    b.HasOne("BabyBug.Data.Models.BabyBugUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BabyBug.Data.Models.OrderProducts.OrderAccessories", b =>
                {
                    b.HasOne("BabyBug.Data.Models.Products.Accessory", "Accessory")
                        .WithMany("OrderAccessories")
                        .HasForeignKey("AccessoryId");

                    b.HasOne("BabyBug.Data.Models.Order", "Order")
                        .WithMany("OrderAccessories")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BabyBug.Data.Models.OrderProducts.OrderGarments", b =>
                {
                    b.HasOne("BabyBug.Data.Models.Products.Garment", "Garment")
                        .WithMany("OrderGarments")
                        .HasForeignKey("GarmentId");

                    b.HasOne("BabyBug.Data.Models.Order", "Order")
                        .WithMany("OrderGarments")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BabyBug.Data.Models.OrderProducts.OrderShoes", b =>
                {
                    b.HasOne("BabyBug.Data.Models.Order", "Order")
                        .WithMany("OrderShoes")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BabyBug.Data.Models.Products.Shoe", "Shoe")
                        .WithMany("OrderShoes")
                        .HasForeignKey("ShoeId");
                });

            modelBuilder.Entity("BabyBug.Data.Models.ProductSpecifications.AccessorySpecification", b =>
                {
                    b.HasOne("BabyBug.Data.Models.Products.Accessory", "Accessory")
                        .WithMany("Specifications")
                        .HasForeignKey("AccessoryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BabyBug.Data.Models.ProductSize", "ProductSize")
                        .WithMany("AccessorySpecifications")
                        .HasForeignKey("ProductSizeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BabyBug.Data.Models.ProductSpecifications.GarmentSpecification", b =>
                {
                    b.HasOne("BabyBug.Data.Models.Products.Garment", "Garment")
                        .WithMany("Specifications")
                        .HasForeignKey("GarmentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BabyBug.Data.Models.ProductSize", "ProductSize")
                        .WithMany("GarmentSpecifications")
                        .HasForeignKey("ProductSizeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BabyBug.Data.Models.ProductSpecifications.ShoeSpecification", b =>
                {
                    b.HasOne("BabyBug.Data.Models.ProductSize", "ProductSize")
                        .WithMany("ShoeSpecifications")
                        .HasForeignKey("ProductSizeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BabyBug.Data.Models.Products.Shoe", "Shoe")
                        .WithMany("Specifications")
                        .HasForeignKey("ShoeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BabyBug.Data.Models.Products.Accessory", b =>
                {
                    b.HasOne("BabyBug.Data.Models.Categories.AccessoryCategory", "Category")
                        .WithMany("Accessories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BabyBug.Data.Models.Products.Garment", b =>
                {
                    b.HasOne("BabyBug.Data.Models.Categories.GarmentCategory", "Category")
                        .WithMany("Garments")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BabyBug.Data.Models.Products.Shoe", b =>
                {
                    b.HasOne("BabyBug.Data.Models.Categories.ShoeCategory", "Category")
                        .WithMany("Shoes")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("BabyBug.Data.Models.BabyBugUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("BabyBug.Data.Models.BabyBugUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BabyBug.Data.Models.BabyBugUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("BabyBug.Data.Models.BabyBugUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
