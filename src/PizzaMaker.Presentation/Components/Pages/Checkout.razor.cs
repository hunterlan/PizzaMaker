using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Components;
using PizzaMaker.Presentation.Models;
using PizzaMaker.Presentation.Models.Orders;
using PizzaMaker.Presentation.ViewModels;

namespace PizzaMaker.Presentation.Components.Pages;

public partial class Checkout : ComponentBase
{
    [SupplyParameterFromForm] private DeliveryForm DeliveryForm { get; set; } = new();
    [SupplyParameterFromForm] private PaymentForm PaymentForm { get; set; } = new();
    [CascadingParameter]
    protected CatalogViewModel CatalogViewModel { get; set; } = new();
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

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            _sessionId = await SessionService.GetSessionIdAsync();
            _userSession = await SessionService.GetSessionAsync(_sessionId);
        }
    }
    private void ValidSubmission()
    {
        Console.WriteLine("ValidSubmission");
    }
}

internal class DeliveryForm
{
    [Required]
    public string? Fullname { get; set; }
    
    [Required]
    public string? PhoneNumber { get; set; }
    
    [Required]
    public string? Address { get; set; }
    
    public string? Note { get; set; }
}

internal class PaymentForm
{
    public PaymentType? PaymentMethod { get; set; }
}