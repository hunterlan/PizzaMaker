namespace PizzaMaker.Presentation.Models.Pizzas;

// Based on tomato, cream sauce, vegeterian, spicy 
public class Category
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Item> Pizzas { get; set; } = [];
}