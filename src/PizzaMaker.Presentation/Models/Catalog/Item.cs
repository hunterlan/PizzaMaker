namespace PizzaMaker.Presentation.Models.Catalog;

public class Item
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public decimal Price { get; set; }
    public required string Description { get; set; }
    public string? Filepath { get; set; }
    public ICollection<Label> Label { get; set; } = [];
}