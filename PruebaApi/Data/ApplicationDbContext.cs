﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PruebaApi.Models;

namespace PruebaApi.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Vendor> Vendors { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Seed roles
            List<IdentityRole> roles =
            [
                new() { Id = "Admin", Name = "Admin", NormalizedName = "ADMIN" },
                new() { Id = "User", Name = "User", NormalizedName = "USER" }
            ];
            builder.Entity<IdentityRole>().HasData(roles);

            // Seed products
            List<Product> products =
            [
                new() { Id = 1, Name = "Product 1", Description = "Description 1", Price = 10.00M },
                new() { Id = 2, Name = "Product 2", Description = "Description 2", Price = 20.00M },
                new() { Id = 3, Name = "Product 3", Description = "Description 3", Price = 30.00M }
            ];
            builder.Entity<Product>().HasData(products);

            // Seed comments
            List<Comment> comments =
            [
                new()
                {
                    Id = 1, ProductId = 1, Title = "Great Product", Content = "I really liked this product!",
                    CreatedAt = new DateTime(2025, 7, 24)
                },
                new()
                {
                    Id = 2, ProductId = 1, Title = "Not bad", Content = "It works as expected.",
                    CreatedAt = new DateTime(2025, 7, 25)
                },
                new()
                {
                    Id = 3, ProductId = 2, Title = "Could be better", Content = "Had some issues.",
                    CreatedAt = new DateTime(2025, 7, 25)
                },
                new()
                {
                    Id = 4, ProductId = 3, Title = "Excellent", Content = "Highly recommend!",
                    CreatedAt = new DateTime(2025, 7, 26)
                }
            ];
            builder.Entity<Comment>().HasData(comments);

            // Seed vendors (without navigation properties)
            List<Vendor> vendors =
            [
                new()
                {
                    Id = 1, Name = "Tienda Tia Maria", Address = "Av Santa Fe 123", Email = "tiamaria123@mail.com",
                    Phone = 1144224422, CreatedAt = new DateTime(2025, 7, 24)
                },
                new()
                {
                    Id = 2, Name = "Bodegón Vinos", Address = "Azcuenaga 456", Email = "bodegon456@mail.com",
                    Phone = 1144556677, CreatedAt = new DateTime(2025, 7, 24)
                }
            ];
            builder.Entity<Vendor>().HasData(vendors);

            // Seed many-to-many relationship for Vendor-Product
            builder.Entity<Vendor>()
                .HasMany(v => v.Products)
                .WithMany(p => p.Vendors)
                .UsingEntity(j => j.ToTable("VendorProducts").HasData(
                    new { VendorsId = 1, ProductsId = 1 },
                    new { VendorsId = 1, ProductsId = 2 },
                    new { VendorsId = 2, ProductsId = 2 }
                ));

            // Configure one-to-many relationship: Product has many Comments
            builder.Entity<Product>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Product)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}