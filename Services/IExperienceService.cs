using Nexora.Models;

namespace Nexora.Services;

public interface IExperienceService
{
    Task<List<ExperienceItem>> GetFeaturedAsync();
    Task<List<ExperienceItem>> GetAllAsync(string? category = null);
    Task<ExperienceItem?> GetByIdAsync(int id);
    Task<List<string>> GetCategoriesAsync();
    Task<List<ExperienceItem>> SearchAsync(string query);
    Task<ExperienceItem> CreateAsync(ExperienceItem item);
    Task<bool> UpdateAsync(ExperienceItem item);
    Task<bool> DeleteAsync(int id);
}
