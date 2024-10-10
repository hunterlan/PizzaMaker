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
    private List<CartItemViewModel>? _cartItems;
    private string? _sessionId = null;
    private Session? _userSession = null;

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
            
            _cartItems = [];
            if (_userSession.Items.Count > 0)
            {
                foreach (var cartItem in _userSession.Items)
                {
                    var currentItem = CatalogViewModel!.Items.First(i => i.Id == cartItem.PizzaId);
                    _cartItems.Add(new CartItemViewModel(currentItem, cartItem.Quantity));
                }
            }

            if (_cartItems.Count > 0)
            {
                StateHasChanged();
            }
            else
            {
                NavManager.NavigateTo("/cart");
            }
        }
    }
    private decimal GetTotalPriceForItem(CartItemViewModel item)
    {
        var itemData = CatalogViewModel!.Items.First(i => i.Id == item.Id);
        return itemData.Price * item.Quantity;
    }

    private decimal GetTotalPrice()
    {
        decimal totalPrice = 0;
        foreach (var cartItem in _cartItems!)
        {
            var itemData = CatalogViewModel!.Items.First(i => i.Id == cartItem.Id);
            totalPrice += itemData.Price * cartItem.Quantity;
        }

        return totalPrice;
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