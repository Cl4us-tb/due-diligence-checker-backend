using DueDiligenceChecker.Shared.Domain.Repositories;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;
using DueDiligenceChecker.Suppliers.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Suppliers.Domain.Repositories;

public interface ISupplierScreeningRepository : IBaseRepository<SupplierScreening>
{
    Task<IEnumerable<SupplierScreening>> ListBySupplierIdAsync(int supplierId);
    Task<IEnumerable<SupplierScreening>> ListBySupplierIdAsync(int supplierId, ScreeningSource? source, int page, int limit);
}
