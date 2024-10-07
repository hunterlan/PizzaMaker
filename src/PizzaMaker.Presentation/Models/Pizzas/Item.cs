namespace PizzaMaker.Presentation.Models.Pizzas;

public class Item
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public string? Filepath { get; set; }
    public List<Category> Categories = [];
}