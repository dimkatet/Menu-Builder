using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Menu_Builder
{
    public class DishContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Bunch> Bunches { get; set; }

        public DishContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Dishes.db");
        }

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Dish>().Property<string>("TenantId").HasField("_tenantId");

            // Configure entity filters
            modelBuilder.Entity<Dish>().HasQueryFilter(b => EF.Property<string>(b, "TenantId") == _tenantId);
            modelBuilder.Entity<Dish>().HasQueryFilter(p => !p.IsDeleted);
        } */
    }

    public class Dish
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Category { get; set; }

        public string Products { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int Amount { get; set; }
    }

    public class Bunch
    {
        public int Id { get; set; }

        public int DishId { get; set; }

        public int ProductId { get; set; }
    }
}
