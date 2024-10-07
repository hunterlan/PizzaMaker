using PizzaMaker.Presentation.Models.Pizzas;

namespace PizzaMaker.Presentation.Components.Pages;

public partial class Home
{
    private IEnumerable<Pizza> Pizzas = [];

    protected override Task OnInitializedAsync()
    {
        Pizzas =
        [
            new Pizza
            {
                Name = "Margherita",
                Description = "Margherita pizza",
                Price = 29.00m
            },
            new Pizza
            {
                Name = "Diavola",
                Description = "Diavola pizza",
                Price = 34.00m
            },
            new Pizza
            {
                Name = "Napoletana",
                Description = "Napoletana pizza",
                Price = 37.00m
            }
        ];
        return base.OnInitializedAsync();
    }
}