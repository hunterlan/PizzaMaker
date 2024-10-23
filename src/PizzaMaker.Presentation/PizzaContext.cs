using Microsoft.EntityFrameworkCore;
using PizzaMaker.Presentation.Models.Catalog;
using PizzaMaker.Presentation.Models.Orders;

namespace PizzaMaker.Presentation;

public class PizzaContext(DbContextOptions<PizzaContext> options) : DbContext(options)
{
    public DbSet<Item> Items { get; set; }
    public DbSet<Label> Labels { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }
    public DbSet<PaymentType> PaymentTypes { get; set; }
    public DbSet<CheckoutItem> CheckoutItem { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Checkout>()
            .HasMany<Item>(c => c.Items)
            .WithMany()
            .UsingEntity<CheckoutItem>();
    }
}