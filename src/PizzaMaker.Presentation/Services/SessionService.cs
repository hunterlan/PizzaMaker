using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Caching.Distributed;
using PizzaMaker.Presentation.Extensions;
using PizzaMaker.Presentation.Models;

namespace PizzaMaker.Presentation.Services;

public class SessionService(ProtectedSessionStorage protectedSessionStorage, IDistributedCache distributedCache) : ISessionService
{
    public async Task<string> GetSessionIdAsync()
    {
        string sessionId;
        var sessionIdResult = await protectedSessionStorage.GetAsync<string>("sessionId");
        if (!sessionIdResult.Success)
        {
            sessionId = Ulid.NewUlid().ToString();
            await protectedSessionStorage.SetAsync("sessionId", sessionId);
        }
        else
        {
            sessionId = sessionIdResult.Value!;
        }

        return sessionId;
    }

    public async Task<Session> GetSessionAsync(string sessionId)
    {
        return await distributedCache.GetAsync<Session>(sessionId!) ?? new Session();
    }

    public async Task<bool> SetSessionAsync(Session session, string sessionId)
    {
        var isSetSuccess = true;
        
        try
        {
            await distributedCache.SetAsync(sessionId, session);
        }
        catch (Exception)
        {
            isSetSuccess = false;
        }
        
        return isSetSuccess;
    }
}