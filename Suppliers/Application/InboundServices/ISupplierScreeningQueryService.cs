using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;
using DueDiligenceChecker.Suppliers.Domain.Model.Queries;

namespace DueDiligenceChecker.Suppliers.Application.InboundServices;

public interface ISupplierScreeningQueryService
{
    Task<IEnumerable<SupplierScreening>> Handle(GetSupplierScreeningsBySupplierIdQuery query, CancellationToken cancellationToken = default);
}
