using Microsoft.AspNetCore.Components;
using PizzaMaker.Presentation.Extensions;
using PizzaMaker.Presentation.Models;
using PizzaMaker.Presentation.Models.Catalog;
using PizzaMaker.Presentation.Models.Orders;
using PizzaMaker.Presentation.ViewModels;

namespace PizzaMaker.Presentation.Components.Pages;

public partial class Home
{
    [CascadingParameter]
    protected CatalogViewModel? CatalogViewModel { get; set; }
    //private IEnumerable<Item> _pizzas = [];
    private string? _sessionId;

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender)
        {
            var sessionIdResult = await ProtectedSessionStore.GetAsync<string>("sessionId");
            if (!sessionIdResult.Success)
            {
                _sessionId = Ulid.NewUlid().ToString();
                await ProtectedSessionStore.SetAsync("sessionId", _sessionId);
            }
            else
            {
                _sessionId = sessionIdResult.Value;
            }   
        }
    }

    private async Task AddPizza(Item item)
    {
        var userSessionData = await Cache.GetAsync<Session>(_sessionId!) ?? new Session();
        var currentCartItems = userSessionData.Items;
        CartItem? cartItem;
        
        if (currentCartItems.Count != 0)
        {
            cartItem = currentCartItems.FirstOrDefault(ci => ci.PizzaId == item.Id);
            if (cartItem is not null)
            {
                userSessionData.Items.Remove(cartItem);
                cartItem.Quantity++;
            }
            else
            {
                cartItem = new CartItem
                {
                    PizzaId = item.Id,
                    Quantity = 1
                };    
            }
        }
        else
        {
            cartItem = new CartItem
            {
                PizzaId = item.Id,
                Quantity = 1
            };
        }

        userSessionData.Items.Add(cartItem);
        await Cache.SetAsync(_sessionId!, userSessionData);
        CatalogViewModel!.InvokeCartChange();
    }
}