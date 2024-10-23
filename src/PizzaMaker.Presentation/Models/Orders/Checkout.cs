using PizzaMaker.Presentation.Models.Catalog;

namespace PizzaMaker.Presentation.Models.Orders;

public class Checkout
{
    public int Id { get; set; }
    
    public string Fullname { get; set; }
    
    public string Address { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public int PaymentTypeId { get; set; }
    public PaymentType PaymentType { get; set; }
    
    public int? UserId { get; set; }
    public string Note { get; set; }
    
    public ICollection<Item> Items { get; set; } = new List<Item>();
}