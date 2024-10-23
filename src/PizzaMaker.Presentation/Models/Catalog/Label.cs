namespace PizzaMaker.Presentation.Models.Catalog;

// Based on tomato, cream sauce, vegeterian, spicy 
public class Label
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public ICollection<Item> Item { get; set; } = [];
}