using DueDiligenceChecker.Suppliers.Domain.Model.Entities;
using DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Transform;

public static class SupplierResponseFromEntityAssembler
{
    public static SupplierResponse ToResponseFromEntity(Supplier supplier)
    {
        var representatives = supplier.Representatives
            .Select(r => new RepresentativeResponse(r.RepresentativeId, r.Role, r.FirstName, r.LastName, r.Age, r.Nationality))
            .ToList();

        return new SupplierResponse(
            supplier.SupplierId,
            supplier.CorporateName,
            supplier.TradeName,
            supplier.TaxId.Value,
            supplier.PhoneNumber,
            supplier.Email,
            supplier.WebSite,
            supplier.PhysicalAddress,
            supplier.Country,
            supplier.Billing.Amount,
            supplier.CreatedAt,
            supplier.UpdatedAt,
            supplier.CreatedBy,
            representatives);
    }
}
