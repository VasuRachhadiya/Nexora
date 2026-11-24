using Microsoft.EntityFrameworkCore;
using Nexora.Data;
using Nexora.Models;

namespace Nexora.Services;

public interface IServiceOfferingService
{
    Task<List<ServiceOffering>> GetAllAsync();
    Task<ServiceOffering?> GetByIdAsync(int id);
}

public class ServiceOfferingService : IServiceOfferingService
{
    private readonly ApplicationDbContext _context;

    public ServiceOfferingService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<ServiceOffering>> GetAllAsync()
    {
        return await _context.Services.OrderBy(s => s.Order).ToListAsync();
    }

    public async Task<ServiceOffering?> GetByIdAsync(int id)
    {
        return await _context.Services.FindAsync(id);
    }
}
