using Microsoft.EntityFrameworkCore;
using Services.ProductsAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.ProductsAPI.DBContexts
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {}

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 1,
                Name = "Product 1",
                Description = "My Description 1",
                ImageUrl="",
                CategoryName ="PC",
                Price = 100.34
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 2,
                Name = "Product 2",
                Description = "My Description 2",
                ImageUrl = "",
                CategoryName = "PC",
                Price = 200.34
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 3,
                Name = "Product 3",
                Description = "My Description 3",
                ImageUrl = "",
                CategoryName = "Laptop",
                Price = 300.34
            });
            modelBuilder.Entity<Product>().HasData(new Product
            {
                ProductId = 4,
                Name = "Product 4",
                Description = "My Description 4",
                ImageUrl = "",
                CategoryName = "Laptop",
                Price = 400.44
            });

        }
    }
}
