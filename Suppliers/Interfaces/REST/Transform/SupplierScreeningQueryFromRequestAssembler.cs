using DueDiligenceChecker.Suppliers.Domain.Model.Queries;
using DueDiligenceChecker.Suppliers.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Transform;

public static class SupplierScreeningQueryFromRequestAssembler
{
    public static GetSupplierScreeningsBySupplierIdQuery ToQuery(int supplierId, int page, int limit, ScreeningSource? source)
    {
        return new GetSupplierScreeningsBySupplierIdQuery(supplierId, page, limit, source);
    }
}

