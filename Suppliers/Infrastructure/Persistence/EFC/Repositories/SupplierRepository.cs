using DueDiligenceChecker.Shared.Infrastructure.Persistence.EFC.Configuration;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities;
using DueDiligenceChecker.Suppliers.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DueDiligenceChecker.Suppliers.Infrastructure.Persistence.EFC.Repositories;

public class SupplierRepository : ISupplierRepository
{
    private readonly AppDbContext _context;

    public SupplierRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Supplier entity)
    {
        await _context.Set<Supplier>().AddAsync(entity);
    }

    public async Task<Supplier?> FindByIdAsync(int id)
    {
        return await _context.Set<Supplier>()
            .Include(s => s.Representatives)
            .FirstOrDefaultAsync(s => s.SupplierId == id);
    }

    public void Update(Supplier entity)
    {
        _context.Set<Supplier>().Update(entity);
    }

    public void Remove(Supplier entity)
    {
        _context.Set<Supplier>().Remove(entity);
    }

    public async Task<IEnumerable<Supplier>> ListAsync()
    {
        return await _context.Set<Supplier>()
            .Include(s => s.Representatives)
            .ToListAsync();
    }
}
