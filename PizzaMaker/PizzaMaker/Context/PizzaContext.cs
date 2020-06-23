using Microsoft.EntityFrameworkCore;
using PizzaMaker.Models;

namespace PizzaMaker.Context
{
    public class PizzaContext : DbContext
    {
        public DbSet<Pizza> Pizzas { get; set; }

        public PizzaContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Pizza>().Property(u => u.Image).HasColumnType("MediumBlob");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("Server=localhost;Database=PizzaMaker;UserId=root;password=;");
        }
    }
}
