using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;

namespace Nexora.Services;

public interface ITestimonialService
{
    Task<List<Testimonial>> GetFeaturedAsync();
    Task<List<Testimonial>> GetAllAsync();
}

public class TestimonialService : ITestimonialService
{
    private readonly ApplicationDbContext _context;

    public TestimonialService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Testimonial>> GetFeaturedAsync()
    {
        return await _context.Testimonials
            .Where(t => t.IsFeatured)
            .OrderBy(t => t.Order)
            .ToListAsync();
    }

    public async Task<List<Testimonial>> GetAllAsync()
    {
        return await _context.Testimonials
            .OrderBy(t => t.Order)
            .ToListAsync();
    }
}
