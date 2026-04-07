using DueDiligenceChecker.Suppliers.Domain.Model.Entities;
using DueDiligenceChecker.Suppliers.Domain.Model.Queries;

namespace DueDiligenceChecker.Suppliers.Application.InboundServices;

public interface ISupplierQueryService
{
    Task<Supplier?> Handle(GetSupplierByIdQuery query);
    Task<IEnumerable<Supplier>> Handle(GetAllSuppliersQuery query);
}
