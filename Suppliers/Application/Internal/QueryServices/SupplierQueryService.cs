using DueDiligenceChecker.Suppliers.Application.InboundServices;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities;
using DueDiligenceChecker.Suppliers.Domain.Model.Queries;
using DueDiligenceChecker.Suppliers.Domain.Repositories;

namespace DueDiligenceChecker.Suppliers.Application.Internal.QueryServices;

public class SupplierQueryService : ISupplierQueryService
{
    private readonly ISupplierRepository _supplierRepository;

    public SupplierQueryService(ISupplierRepository supplierRepository)
    {
        _supplierRepository = supplierRepository;
    }

    public async Task<Supplier?> Handle(GetSupplierByIdQuery query)
    {
        return await _supplierRepository.FindByIdAsync(query.SupplierId);
    }

    public async Task<IEnumerable<Supplier>> Handle(GetAllSuppliersQuery query)
    {
        var suppliers = await _supplierRepository.ListAsync();

        IEnumerable<Supplier> result = suppliers;

        if (!string.IsNullOrWhiteSpace(query.SearchTerm))
        {
            var term = query.SearchTerm.Trim();
            result = result.Where(s =>
                s.CorporateName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                s.TradeName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                s.TaxId.Value.Contains(term, StringComparison.OrdinalIgnoreCase));
        }

        result = result.OrderByDescending(s => s.UpdatedAt ?? s.CreatedAt);

        var page = query.Page < 1 ? 1 : query.Page;
        var limit = query.Limit < 1 ? 10 : query.Limit;

        return result.Skip((page - 1) * limit).Take(limit).ToList();
    }
}
