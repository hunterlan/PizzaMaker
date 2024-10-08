using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using PizzaMaker.Presentation.Models;
using PizzaMaker.Presentation.Models.Orders;

namespace PizzaMaker.Presentation.Components.Pages;

public partial class Checkout : ComponentBase
{
    [SupplyParameterFromForm] private CheckoutForm Form { get; set; } = new();
    private List<PaymentType> PaymentMethods { get; set; } = [];
    private string? _sessionId = null;
    private Session? _userSession = null;
    private decimal TotalPrice = 0;

    protected override void OnInitialized()
    {
        PaymentMethods = [
            new PaymentType
            {
              Id = 1,
              IsImmediate = false,
              Name = "Cash"
            },
            new PaymentType
            {
                Id = 2,
                IsImmediate = true,
                Name = "Visa/Mastercard"
            }
        ];
        base.OnInitialized();
    }

    /*protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            
        }
    }*/
}

internal class CheckoutForm
{
    [Required]
    public string? Fullname { get; set; }
    
    [Required]
    public string? PhoneNumber { get; set; }
    
    [Required]
    public string? Address { get; set; }
    
    public string? Note { get; set; }
    
    public string? SelectedPaymentMethod { get; set; }
}