using DueDiligenceChecker.Shared.Infrastructure.Persistence.EFC.Configuration;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;
using DueDiligenceChecker.Suppliers.Domain.Model.ValueObjects;
using DueDiligenceChecker.Suppliers.Domain.Repositories;
using DueDiligenceChecker.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DueDiligenceChecker.Suppliers.Infrastructure.Persistence.EFC.Repositories;

public class SupplierScreeningRepository : BaseRepository<SupplierScreening>, ISupplierScreeningRepository
{
    public SupplierScreeningRepository(AppDbContext context) : base(context)
    {
    }

    public override async Task<SupplierScreening?> FindByIdAsync(int id)
    {
        return await Context.Set<SupplierScreening>()
            .Include(s => s.InterpolHits)
            .Include(s => s.SecopHits)
            .Include(s => s.SmvHits)
            .FirstOrDefaultAsync(s => s.SupplierScreeningId == id);
    }

    public override async Task<IEnumerable<SupplierScreening>> ListAsync()
    {
        return await Context.Set<SupplierScreening>()
            .Include(s => s.InterpolHits)
            .Include(s => s.SecopHits)
            .Include(s => s.SmvHits)
            .ToListAsync();
    }

    public async Task<IEnumerable<SupplierScreening>> ListBySupplierIdAsync(int supplierId)
    {
        return await Context.Set<SupplierScreening>()
            .Where(s => s.SupplierId == supplierId)
            .OrderByDescending(s => s.ExecutedAt)
            .Include(s => s.InterpolHits)
            .Include(s => s.SecopHits)
            .Include(s => s.SmvHits)
            .ToListAsync();
    }

    public async Task<IEnumerable<SupplierScreening>> ListBySupplierIdAsync(int supplierId, ScreeningSource? source, int page, int limit)
    {
        var query = Context.Set<SupplierScreening>()
            .Where(s => s.SupplierId == supplierId);

        if (source == ScreeningSource.Interpol)
            query = query.Where(s => s.InterpolHits.Any());
        else if (source == ScreeningSource.Secop)
            query = query.Where(s => s.SecopHits.Any());
        else if (source == ScreeningSource.Smv)
            query = query.Where(s => s.SmvHits.Any());

        return await query
            .OrderByDescending(s => s.ExecutedAt)
            .Include(s => s.InterpolHits)
            .Include(s => s.SecopHits)
            .Include(s => s.SmvHits)
            .Skip((page - 1) * limit)
            .Take(limit)
            .ToListAsync();
    }
}
