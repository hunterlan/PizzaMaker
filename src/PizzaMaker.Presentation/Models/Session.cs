using PizzaMaker.Presentation.Models.Pizzas;
using PizzaMaker.Presentation.Models.Users;

namespace PizzaMaker.Presentation.Models;

public class Session
{
    public User? CurrentUser { get; set; }
    public List<Item> Items { get; set; } = [];
}