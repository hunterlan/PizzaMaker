using PizzaMaker.Presentation.Models.Catalog;

namespace PizzaMaker.Presentation.Services;

public interface ICatalogService
{
    IEnumerable<Item> GetItems();
}