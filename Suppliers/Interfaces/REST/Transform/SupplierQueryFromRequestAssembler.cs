using DueDiligenceChecker.Suppliers.Domain.Model.Queries;

namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Transform;

public static class SupplierQueryFromRequestAssembler
{
    public static GetSupplierByIdQuery ToQuery(int supplierId)
    {
        return new GetSupplierByIdQuery(supplierId);
    }

    public static GetAllSuppliersQuery ToQuery(int page, int limit, string? searchTerm)
    {
        return new GetAllSuppliersQuery(page, limit, searchTerm);
    }
}
