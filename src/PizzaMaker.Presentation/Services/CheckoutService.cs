using PizzaMaker.Presentation.Models.Catalog;
using PizzaMaker.Presentation.Models.Orders;

namespace PizzaMaker.Presentation.Services;

public class CheckoutService(PizzaContext context) : ICheckoutService
{
    public async Task CreateAsync(Checkout checkout, Dictionary<Item, int> itemCount)
    {
        var transaction = await context.Database.BeginTransactionAsync();
        context.Checkouts.Add(checkout);
        context.SaveChanges();
        foreach (var itemCountInfo in itemCount)
        {
            var checkoutItem = new CheckoutItem {CheckoutId = checkout.Id, ItemId = itemCountInfo.Key.Id, Quantity = itemCountInfo.Value};
            context.CheckoutItem.Add(checkoutItem);
        }
        context.SaveChanges();
        await transaction.CommitAsync();
    }
}