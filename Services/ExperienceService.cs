using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;

namespace Nexora.Services;

public class ExperienceService : IExperienceService
{
    private readonly ApplicationDbContext _context;

    public ExperienceService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ExperienceItem>> GetFeaturedAsync()
    {
        return await _context.Experiences
            .Where(e => e.IsFeatured)
            .OrderBy(e => e.DisplayOrder)
            .ToListAsync();
    }

    public async Task<List<ExperienceItem>> GetAllAsync(string? category = null)
    {
        var query = _context.Experiences.AsQueryable();
        if (!string.IsNullOrWhiteSpace(category) && category != "All")
        {
            query = query.Where(e => e.Category == category);
        }
        return await query.OrderBy(e => e.DisplayOrder).ToListAsync();
    }

    public async Task<ExperienceItem?> GetByIdAsync(int id)
    {
        return await _context.Experiences.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<string>> GetCategoriesAsync()
    {
        return await _context.Experiences
            .Select(e => e.Category)
            .Distinct()
            .OrderBy(c => c)
            .ToListAsync();
    }

    public async Task<List<ExperienceItem>> SearchAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return await GetAllAsync();

        var lowerQuery = query.ToLower();
        return await _context.Experiences
            .Where(e => e.Title.ToLower().Contains(lowerQuery) ||
                        e.Subtitle.ToLower().Contains(lowerQuery) ||
                        e.Category.ToLower().Contains(lowerQuery) ||
                        e.Description.ToLower().Contains(lowerQuery) ||
                        (e.TechStack != null && e.TechStack.ToLower().Contains(lowerQuery)))
            .OrderBy(e => e.DisplayOrder)
            .ToListAsync();
    }

    public async Task<ExperienceItem> CreateAsync(ExperienceItem item)
    {
        _context.Experiences.Add(item);
        await _context.SaveChangesAsync();
        return item;
    }

    public async Task<bool> UpdateAsync(ExperienceItem item)
    {
        var existing = await _context.Experiences.FindAsync(item.Id);
        if (existing == null) return false;

        existing.Title = item.Title;
        existing.Subtitle = item.Subtitle;
        existing.Category = item.Category;
        existing.Description = item.Description;
        existing.Client = item.Client;
        existing.Year = item.Year;
        existing.TechStack = item.TechStack;
        existing.MetricResult = item.MetricResult;
        existing.ImageUrl = item.ImageUrl;
        existing.LiveDemoUrl = item.LiveDemoUrl;
        existing.AccentColor = item.AccentColor;
        existing.IsFeatured = item.IsFeatured;
        existing.DisplayOrder = item.DisplayOrder;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var item = await _context.Experiences.FindAsync(id);
        if (item == null) return false;

        _context.Experiences.Remove(item);
        await _context.SaveChangesAsync();
        return true;
    }
}
