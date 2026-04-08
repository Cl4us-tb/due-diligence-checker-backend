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
        var page = query.Page < 1 ? 1 : query.Page;
        var limit = query.Limit < 1 ? 10 : query.Limit;

        return await _supplierScreeningRepository.ListBySupplierIdAsync(query.SupplierId, query.Source, page, limit);
    }
}
