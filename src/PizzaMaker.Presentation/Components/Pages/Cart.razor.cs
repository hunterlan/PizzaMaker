using Microsoft.AspNetCore.Components;
using PizzaMaker.Presentation.Models;
using PizzaMaker.Presentation.Models.Pizzas;
using PizzaMaker.Presentation.ViewModels;

namespace PizzaMaker.Presentation.Components.Pages;

public partial class Cart : ComponentBase
{
    private List<CartItemViewModel>? _cartItems;
    private List<Item> _items = [];
    private string? _sessionId = null;
    private Session? _userSession = null;

    protected override Task OnInitializedAsync()
    {
        _items =
        [
            new Item
            {
                Name = "Margherita",
                Description = "Margherita pizza",
                Price = 29.00m
            },
            new Item
            {
                Name = "Diavola",
                Description = "Diavola pizza",
                Price = 34.00m
            },
            new Item
            {
                Name = "Napoletana",
                Description = "Napoletana pizza",
                Price = 37.00m
            }
        ];
        return base.OnInitializedAsync();
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
                    var currentItem = _items.First(i => i.Id == cartItem.PizzaId);
                    _cartItems.Add(new CartItemViewModel(currentItem, cartItem.Quantity));
                }
            }

            if (_cartItems.Count > 0)
            {
                StateHasChanged();
            }
        }
    }

    private decimal GetTotalPriceForItem(CartItemViewModel item)
    {
        return 0;
    }

    private async Task ChangeQuantity(bool isNegative, CartItemViewModel item)
    {
        var userItem = _userSession!.Items.First(i => i.PizzaId == item.Id);
        _userSession.Items.Remove(userItem);
        
        if (isNegative)
        {
            item.Quantity--;
        }
        else
        {
            item.Quantity++;
        }
        
        userItem.Quantity = item.Quantity;
        _userSession.Items.Add(userItem);
        
        await SessionService.SetSessionAsync(_userSession, _sessionId!);
        
        StateHasChanged();
    }
}