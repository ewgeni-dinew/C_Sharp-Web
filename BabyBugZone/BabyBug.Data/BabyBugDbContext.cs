using BabyBug.Data.Models;
using BabyBug.Data.Models.Categories;
using BabyBug.Data.Models.OrderProducts;
using BabyBug.Data.Models.Products;
using BabyBug.Data.Models.ProductSpecifications;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BabyBugZone.Data
{
    public class BabyBugDbContext : IdentityDbContext<BabyBugUser>
    {
        public DbSet<BabyBugUser> BabyBugUsers { get; set; }
        public DbSet<GarmentCategory> GarmentCategories { get; set; }
        public DbSet<ShoeCategory> ShoeCategories { get; set; }
        public DbSet<AccessoryCategory> AccessoryCategories { get; set; }
        public DbSet<Garment> Garments { get; set; }
        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Accessory> Accessories { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<GarmentSpecification> GarmentSpecifications { get; set; }
        public DbSet<ShoeSpecification> ShoeSpecifications { get; set; }
        public DbSet<AccessorySpecification> AccessorySpecifications { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderGarments> OrderGarments { get; set; }
        public DbSet<OrderShoes> OrderShoes { get; set; }
        public DbSet<OrderAccessories> OrderAccessories { get; set; }
        public DbSet<BlogPage> BlogPages { get; set; }

        public BabyBugDbContext(DbContextOptions<BabyBugDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Garment>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Entity<Shoe>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Entity<Accessory>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Entity<Garment>()
                .HasOne(x => x.Category)
                .WithMany(c => c.Garments)
                .HasForeignKey(x => x.CategoryId);

            builder.Entity<Shoe>()
                .HasOne(x => x.Category)
                .WithMany(c => c.Shoes)
                .HasForeignKey(x => x.CategoryId);

            builder.Entity<Accessory>()
                .HasOne(x => x.Category)
                .WithMany(c => c.Accessories)
                .HasForeignKey(x => x.CategoryId);

            //builder.Entity<GarmentSpecification>()
            //    .HasOne(x => x.ProductSize)
            //    .WithMany(g => g.GarmentSpecifications)
            //    .HasForeignKey(x => x.ProductSizeId);

            //builder.Entity<ShoeSpecification>()
            //    .HasOne(x => x.ProductSize)
            //    .WithMany(g => g.ShoeSpecifications)
            //    .HasForeignKey(x => x.ProductSizeId);

            //builder.Entity<AccessorySpecification>()
            //    .HasOne(x => x.ProductSize)
            //    .WithMany(g => g.AccessorySpecifications)
            //    .HasForeignKey(x => x.ProductSizeId);

            builder.Entity<GarmentSpecification>()
                .HasKey(x => new { x.ProductSizeId, x.ProductId });

            builder.Entity<ShoeSpecification>()
                .HasKey(x => new { x.ProductSizeId, x.ProductId });

            builder.Entity<AccessorySpecification>()
                .HasKey(x => new { x.ProductSizeId, x.ProductId });

            builder.Entity<OrderGarments>()
                .HasKey(x => new { x.ProductId, x.OrderId });

            builder.Entity<OrderShoes>()
                .HasKey(x => new { x.ProductId, x.OrderId });

            builder.Entity<OrderAccessories>()
                .HasKey(x => new { x.ProductId, x.OrderId });

            builder.Entity<OrderGarments>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Entity<OrderShoes>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Entity<OrderAccessories>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18, 2)");

            base.OnModelCreating(builder);
        }
    }
}
