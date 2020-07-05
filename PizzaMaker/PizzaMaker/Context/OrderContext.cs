using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PizzaMaker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzaMaker.Context
{
    public class OrderContext : DbContext
    {
        public DbSet<Order> Orders { get; set; }

        public OrderContext()
        {
            Database.EnsureCreated();
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
