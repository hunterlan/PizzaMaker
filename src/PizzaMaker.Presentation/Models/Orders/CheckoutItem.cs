namespace PizzaMaker.Presentation.Models.Orders;

public class CheckoutItem
{
    public int CheckoutId { get; set; }
    public int ItemId { get; set; }
    public int Quantity { get; set; }
}