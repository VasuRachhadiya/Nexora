using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;

namespace Nexora.Services;

public class ContactService : IContactService
{
    private readonly ApplicationDbContext _context;

    public ContactService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ContactMessage> SubmitMessageAsync(ContactMessage message)
    {
        message.CreatedAt = DateTime.UtcNow;
        message.IsRead = false;
        _context.ContactMessages.Add(message);
        await _context.SaveChangesAsync();
        return message;
    }

    public async Task<List<ContactMessage>> GetAllMessagesAsync(bool? isRead = null)
    {
        var query = _context.ContactMessages.AsQueryable();
        if (isRead.HasValue)
        {
            query = query.Where(m => m.IsRead == isRead.Value);
        }
        return await query.OrderByDescending(m => m.CreatedAt).ToListAsync();
    }

    public async Task<ContactMessage?> GetMessageByIdAsync(int id)
    {
        return await _context.ContactMessages.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<bool> MarkAsReadAsync(int id)
    {
        var msg = await _context.ContactMessages.FindAsync(id);
        if (msg == null) return false;

        msg.IsRead = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ArchiveMessageAsync(int id)
    {
        var msg = await _context.ContactMessages.FindAsync(id);
        if (msg == null) return false;

        msg.IsArchived = true;
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteMessageAsync(int id)
    {
        var msg = await _context.ContactMessages.FindAsync(id);
        if (msg == null) return false;

        _context.ContactMessages.Remove(msg);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<int> GetUnreadCountAsync()
    {
        return await _context.ContactMessages.CountAsync(m => !m.IsRead);
    }
}
