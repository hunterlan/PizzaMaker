using PizzaMaker.Presentation.Models;
using PizzaMaker.Presentation.Models.Pizzas;

namespace PizzaMaker.Presentation.ViewModels;

public class CatalogViewModel
{
    public List<Item> Items { get; set; } = [];

    public delegate Task CartCountChanged();
    public event CartCountChanged OnCartCountChanged;

    public void InvokeCartChange()
    {
        OnCartCountChanged.Invoke();
    }
}