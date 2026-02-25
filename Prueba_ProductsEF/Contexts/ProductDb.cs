
using Microsoft.EntityFrameworkCore;
using Prueba_productsEF.Models;
using Prueba_ProductsEF.Models;
using System.Collections.Generic;

namespace Prueba_productsEF.Contexts
{

    public class ProductDb : DbContext
    {
        public ProductDb(DbContextOptions<ProductDb> options)
            : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StockStore> StockStores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rol> Rols { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .Property(p => p.CategoryId)
                .HasDefaultValue(1);

            //Semilla de datos para la categoría "General"
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "General" }
            );

            //Semilla de datos para los usuarios "Admin", "Manager"
            modelBuilder.Entity<Rol>().HasData(
                new Rol { Id = 1, Name = "Admin" }, new Rol { Id = 2, Name = "Manager" }
            );
        }
    }
}