using DueDiligenceChecker.Shared.Infrastructure.Persistence.EFC.Configuration;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities;
using DueDiligenceChecker.Suppliers.Domain.Repositories;
using DueDiligenceChecker.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace DueDiligenceChecker.Suppliers.Infrastructure.Persistence.EFC.Repositories;

public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
{
    public SupplierRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> ExistsByTaxIdAsync(string taxId)
    {
        return await Context.Set<Supplier>().AnyAsync(s => s.TaxId.Value == taxId);
    }

    public override async Task<Supplier?> FindByIdAsync(int id)
    {
        return await Context.Set<Supplier>()
            .Include(s => s.Representatives)
            .FirstOrDefaultAsync(s => s.SupplierId == id);
    }

    public override async Task<IEnumerable<Supplier>> ListAsync()
    {
        return await Context.Set<Supplier>()
            .Include(s => s.Representatives)
            .ToListAsync();
    }
}
