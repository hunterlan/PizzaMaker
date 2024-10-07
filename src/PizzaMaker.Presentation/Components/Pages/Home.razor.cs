using PizzaMaker.Presentation.Models.Pizzas;
using PizzaMaker.Presentation.Extensions;
using PizzaMaker.Presentation.Models;

namespace PizzaMaker.Presentation.Components.Pages;

public partial class Home
{
    private IEnumerable<Item> _pizzas = [];
    private string? _sessionId;
    protected override Task OnInitializedAsync()
    {
        _pizzas =
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

        userSessionData.Items.Add(item);
        await Cache.SetAsync(_sessionId!, userSessionData);
        StateHasChanged();
    }
}