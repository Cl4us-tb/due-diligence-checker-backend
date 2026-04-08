using DueDiligenceChecker.Shared.Infrastructure.Persistence.EFC.Configuration;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;
using DueDiligenceChecker.Suppliers.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DueDiligenceChecker.Suppliers.Infrastructure.Persistence.EFC.Repositories;

public class SupplierScreeningRepository : ISupplierScreeningRepository
{
    private readonly AppDbContext _context;

    public SupplierScreeningRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(SupplierScreening entity)
    {
        await _context.Set<SupplierScreening>().AddAsync(entity);
    }

    public async Task<SupplierScreening?> FindByIdAsync(int id)
    {
        return await _context.Set<SupplierScreening>()
            .Include(s => s.InterpolHits)
            .Include(s => s.SecopHits)
            .Include(s => s.SmvHits)
            .FirstOrDefaultAsync(s => s.SupplierScreeningId == id);
    }

    public void Update(SupplierScreening entity)
    {
        _context.Set<SupplierScreening>().Update(entity);
    }

    public void Remove(SupplierScreening entity)
    {
        _context.Set<SupplierScreening>().Remove(entity);
    }

    public async Task<IEnumerable<SupplierScreening>> ListAsync()
    {
        return await _context.Set<SupplierScreening>()
            .Include(s => s.InterpolHits)
            .Include(s => s.SecopHits)
            .Include(s => s.SmvHits)
            .ToListAsync();
    }

    public async Task<IEnumerable<SupplierScreening>> ListBySupplierIdAsync(int supplierId)
    {
        return await _context.Set<SupplierScreening>()
            .Where(s => s.SupplierId == supplierId)
            .OrderByDescending(s => s.ExecutedAt)
            .Include(s => s.InterpolHits)
            .Include(s => s.SecopHits)
            .Include(s => s.SmvHits)
            .ToListAsync();
    }
}
