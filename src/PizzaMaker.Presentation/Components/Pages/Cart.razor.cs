using Microsoft.AspNetCore.Components;
using PizzaMaker.Presentation.Models;
using PizzaMaker.Presentation.ViewModels;

namespace PizzaMaker.Presentation.Components.Pages;

public partial class Cart : ComponentBase
{
    private List<CartItemViewModel>? _cartItems;
    [CascadingParameter]
    protected CatalogViewModel? CatalogViewModel { get; set; }
    private string? _sessionId = null;
    private Session? _userSession = null;
    
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
        }
    }

    private decimal GetTotalPriceForItem(CartItemViewModel item)
    {
        var itemData = CatalogViewModel!.Items.First(i => i.Id == item.Id);
        return itemData.Price * item.Quantity;
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
        CatalogViewModel!.InvokeCartChange();
    }

    private async Task EmptyCart()
    {
        _userSession!.Items = [];
        _cartItems = [];
        await SessionService.SetSessionAsync(_userSession, _sessionId!);
        CatalogViewModel!.InvokeCartChange();
    }
}