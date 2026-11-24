using Nexora.Models;

namespace Nexora.Services;

public interface INewsletterService
{
    Task<(bool Success, string Message)> SubscribeAsync(string email);
    Task<List<NewsletterSubscriber>> GetAllAsync();
    Task<bool> RemoveAsync(int id);
}
