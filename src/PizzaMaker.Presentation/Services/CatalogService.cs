using Microsoft.EntityFrameworkCore;
using PizzaMaker.Presentation.Models.Catalog;

namespace PizzaMaker.Presentation.Services;

public class CatalogService(PizzaContext context) : ICatalogService
{
    public IEnumerable<Item> GetItems()
    {
        var items = context.Items.Include(i => i.Label).AsEnumerable();
        return items;
    }
}