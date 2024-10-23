using PizzaMaker.Presentation.Models.Catalog;

namespace PizzaMaker.Presentation.ViewModels;

public class CartItemViewModel(Item item, int quantity)
{
    public int Id { get; set; } = item.Id;
    public string Name { get; set; } = item.Name;
    public int Quantity { get; set; } = quantity;
    public string? Filepath { get; set; } = item.Filepath;
}