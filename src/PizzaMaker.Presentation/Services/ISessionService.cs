using PizzaMaker.Presentation.Models;

namespace PizzaMaker.Presentation.Services;

public interface ISessionService
{
    Task<string> GetSessionIdAsync();
    Task<Session> GetSessionAsync(string sessionId);
    Task<bool> SetSessionAsync(Session session, string sessionId);
}