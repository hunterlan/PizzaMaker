using PizzaMaker.Presentation.Models.Catalog;
using PizzaMaker.Presentation.Models.Orders;

namespace PizzaMaker.Presentation.Services;

public interface ICheckoutService
{
    Task CreateAsync(Checkout checkout, Dictionary<Item, int> itemCount);
}