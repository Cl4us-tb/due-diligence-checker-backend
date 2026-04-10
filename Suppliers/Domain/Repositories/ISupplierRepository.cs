using DueDiligenceChecker.Shared.Domain.Repositories;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities;

namespace DueDiligenceChecker.Suppliers.Domain.Repositories;

public interface ISupplierRepository : IBaseRepository<Supplier>
{
    Task<bool> ExistsByTaxIdAsync(string taxId);
}
