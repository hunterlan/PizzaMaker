using Microsoft.EntityFrameworkCore;
using PizzaMaker.Presentation.Models.Catalog;

namespace PizzaMaker.Presentation;

public class PizzaContext(DbContextOptions<PizzaContext> options) : DbContext(options)
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Label> Labels { get; set; }
}