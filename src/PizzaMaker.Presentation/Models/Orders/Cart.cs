namespace PizzaMaker.Presentation.Models.Orders;

public class Cart
{
    public string Guid { get; set; }
    public List<int> PizzasId { get; set; } = [];
}