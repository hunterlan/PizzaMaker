using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PizzaMaker.Models;
using SQLitePCL;

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

        private string GetConnection(string chooseDB)
        {
            string stringConnection = "";

            IConfiguration Configuration = new ConfigurationBuilder().
                AddJsonFile("appsettings.json", false).
                Build();

            string server = Configuration.GetSection(chooseDB).GetSection("server").Value;
            string userDB = Configuration.GetSection(chooseDB).GetSection("UserId").Value;
            string password = Configuration.GetSection(chooseDB).GetSection("password").Value;
            string database = Configuration.GetSection(chooseDB).GetSection("database").Value;

            stringConnection = "Server=" + server + ";UserId=" + userDB + ";password="
                + password + ";database=" + database + ";";

            return stringConnection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(GetConnection("DeveloperHomeDB"));
        }
    }
}
