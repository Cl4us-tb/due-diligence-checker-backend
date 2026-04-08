using DueDiligenceChecker.Shared.Domain.Repositories;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;

namespace DueDiligenceChecker.Suppliers.Domain.Repositories;

public interface ISupplierScreeningRepository : IBaseRepository<SupplierScreening>
{
    Task<IEnumerable<SupplierScreening>> ListBySupplierIdAsync(int supplierId);
}
