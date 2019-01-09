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

        public DbSet<ProductType> ProductTypes { get; set; }

        public DbSet<DeliveryType> DeliveryTypes { get; set; }

        public DbSet<PaymentType> PaymentTypes { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<ProductSize> ProductSizes { get; set; }

        public DbSet<ProductSpecification> ProductSpecifications { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<OrderProduct> OrderProducts { get; set; }

        public DbSet<BlogPage> BlogPages { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<UserReviews> UserReviews { get; set; }

        public BabyBugDbContext(DbContextOptions<BabyBugDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserReviews>()
                .HasKey(x => x.ReviewId);

            builder.Entity<UserReviews>()
                .HasOne(x => x.Review)
                .WithMany(r => r.UserReviews)
                .HasForeignKey(x => x.ReviewId);

            builder.Entity<UserReviews>()
                .HasOne(x => x.Product)
                .WithMany(p => p.UserReviews)
                .HasForeignKey(x => x.ProductId);

            builder.Entity<UserReviews>()
                .HasOne(x => x.User)
                .WithMany(u => u.UserReviews)
                .HasForeignKey(x => x.UserId);

            builder.Entity<DeliveryType>()
                .Property(x => x.Type)
                .IsRequired(false);

            builder.Entity<PaymentType>()
                .Property(x => x.Type)
                .IsRequired(false);

            builder.Entity<ProductType>()
                .HasMany(x => x.Categories)
                .WithOne(c => c.ProductType)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ProductCategory>()
                .HasOne(x => x.ProductType)
                .WithMany(c => c.Categories)
                .HasForeignKey(x => x.ProductTypeId);

            builder.Entity<ProductSize>()
                .HasOne(x => x.ProductType)
                .WithMany(c => c.Sizes)
                .HasForeignKey(x => x.ProductTypeId);

            builder.Entity<Product>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Entity<Product>()
                .HasOne(x => x.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(x => x.CategoryId);

            builder.Entity<ProductSpecification>()
                .HasKey(x => new { x.ProductSizeId, x.ProductId });

            builder.Entity<Order>()
                .HasOne(x => x.DeliveryType)
                .WithMany(c => c.Orders)
                .HasForeignKey(x => x.DeliveryTypeId)
                .IsRequired(false);

            builder.Entity<Order>()
                .HasOne(x => x.PaymentType)
                .WithMany(c => c.Orders)
                .HasForeignKey(x => x.PaymentTypeId)
                .IsRequired(false);

            builder.Entity<OrderProduct>()
                .HasKey(x => new { x.ProductId, x.OrderId, x.Size });

            builder.Entity<OrderProduct>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18, 2)");

            base.OnModelCreating(builder);
        }
    }
}
