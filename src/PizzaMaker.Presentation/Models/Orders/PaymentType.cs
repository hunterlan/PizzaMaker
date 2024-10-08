namespace PizzaMaker.Presentation.Models.Orders;

public class PaymentType
{
    public int Id { get; set; }
    
    public required string Name { get; set; }
    
    public required bool IsImmediate { get; set; }
}