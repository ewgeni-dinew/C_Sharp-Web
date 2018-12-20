﻿using BabyBug.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace BabyBugZone.Data
{
    public class BabyBugDbContext : IdentityDbContext<BabyBugUser>
    {
        public DbSet<BabyBugUser> BabyBugUsers { get; set; }
        public DbSet<GarmentCategory> GarmentCategories { get; set; }
        public DbSet<Garment> Garments { get; set; }
        public DbSet<GarmentSize> GarmentSizes { get; set; }
        public DbSet<GarmentSpecification> GarmentSpecifications { get; set; }

        public BabyBugDbContext(DbContextOptions<BabyBugDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Garment>()
                .Property(x => x.Price)
                .HasColumnType("decimal(18, 2)");

            builder.Entity<Garment>()
                .HasOne(x => x.Category)
                .WithMany(c => c.Garments)
                .HasForeignKey(x => x.CategoryId);

            builder.Entity<GarmentSpecification>()
                .HasKey(x => new { x.GarmentSizeId, x.GarmentId });         

            base.OnModelCreating(builder);
        }
    }
}