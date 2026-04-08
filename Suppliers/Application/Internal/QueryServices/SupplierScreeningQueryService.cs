using DueDiligenceChecker.Suppliers.Application.InboundServices;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;
using DueDiligenceChecker.Suppliers.Domain.Model.Queries;
using DueDiligenceChecker.Suppliers.Domain.Repositories;

namespace DueDiligenceChecker.Suppliers.Application.Internal.QueryServices;

public class SupplierScreeningQueryService : ISupplierScreeningQueryService
{
    private readonly ISupplierScreeningRepository _supplierScreeningRepository;

    public SupplierScreeningQueryService(ISupplierScreeningRepository supplierScreeningRepository)
    {
        _supplierScreeningRepository = supplierScreeningRepository;
    }

    public async Task<IEnumerable<SupplierScreening>> Handle(GetSupplierScreeningsBySupplierIdQuery query, CancellationToken cancellationToken = default)
    {
        return await _supplierScreeningRepository.ListBySupplierIdAsync(query.SupplierId);
    }
}
