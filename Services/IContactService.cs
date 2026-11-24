using Nexora.Models;

namespace Nexora.Services;

public interface IContactService
{
    Task<ContactMessage> SubmitMessageAsync(ContactMessage message);
    Task<List<ContactMessage>> GetAllMessagesAsync(bool? isRead = null);
    Task<ContactMessage?> GetMessageByIdAsync(int id);
    Task<bool> MarkAsReadAsync(int id);
    Task<bool> ArchiveMessageAsync(int id);
    Task<bool> DeleteMessageAsync(int id);
    Task<int> GetUnreadCountAsync();
}
