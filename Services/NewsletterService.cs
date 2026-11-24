using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;

namespace Nexora.Services;

public class NewsletterService : INewsletterService
{
    private readonly ApplicationDbContext _context;

    public NewsletterService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<(bool Success, string Message)> SubscribeAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email) || !email.Contains('@'))
        {
            return (false, "Please provide a valid email address.");
        }

        var normalizedEmail = email.Trim().ToLower();
        var exists = await _context.NewsletterSubscribers.AnyAsync(n => n.Email.ToLower() == normalizedEmail);

        if (exists)
        {
            return (false, "This email is already part of the NEXORA insider dispatch.");
        }

        var subscriber = new NewsletterSubscriber
        {
            Email = normalizedEmail,
            SubscribedAt = DateTime.UtcNow,
            IsActive = true
        };

        _context.NewsletterSubscribers.Add(subscriber);
        await _context.SaveChangesAsync();

        return (true, "Welcome to NEXORA Dispatch. You will receive exclusive design drops and spatial engineering insights.");
    }

    public async Task<List<NewsletterSubscriber>> GetAllAsync()
    {
        return await _context.NewsletterSubscribers
            .OrderByDescending(n => n.SubscribedAt)
            .ToListAsync();
    }

    public async Task<bool> RemoveAsync(int id)
    {
        var item = await _context.NewsletterSubscribers.FindAsync(id);
        if (item == null) return false;

        _context.NewsletterSubscribers.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}
