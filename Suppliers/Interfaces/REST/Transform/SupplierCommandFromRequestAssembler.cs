using DueDiligenceChecker.Suppliers.Domain.Model.Commands;
using DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Transform;

public static class SupplierCommandFromRequestAssembler
{
    public static CreateSupplierCommand ToCommandFromRequest(CreateSupplierRequest request, int createdBy)
    {
        var representatives = request.Representatives?.Select(r =>
            new RepresentativeCommandItem(r.Role, r.FirstName, r.LastName, r.Age, r.Nationality)).ToList();

        return new CreateSupplierCommand(
            request.CorporateName,
            request.TradeName,
            request.TaxIdentification,
            request.PhoneNumber,
            request.Email,
            request.WebSite,
            request.PhysicalAddress,
            request.Country,
            request.AnnualBillingAmount,
            createdBy,
            representatives);
    }

    public static UpdateSupplierCommand ToCommandFromRequest(int supplierId, UpdateSupplierRequest request)
    {
        return new UpdateSupplierCommand(
            supplierId,
            request.CorporateName,
            request.TradeName,
            request.PhoneNumber,
            request.Email,
            request.WebSite,
            request.PhysicalAddress,
            request.Country,
            request.AnnualBillingAmount);
    }

    public static DeleteSupplierCommand ToCommandFromRequest(int supplierId)
    {
        return new DeleteSupplierCommand(supplierId);
    }

    public static AddRepresentativeCommand ToCommandFromRequest(int supplierId, AddRepresentativeRequest request)
    {
        return new AddRepresentativeCommand(supplierId, request.Role, request.FirstName, request.LastName, request.Age, request.Nationality);
    }

    public static UpdateRepresentativeCommand ToCommandFromRequest(int supplierId, int representativeId, UpdateRepresentativeRequest request)
    {
        return new UpdateRepresentativeCommand(supplierId, representativeId, request.Role, request.FirstName, request.LastName, request.Age, request.Nationality);
    }

    public static RemoveRepresentativeCommand ToCommandFromRequest(int supplierId, int representativeId)
    {
        return new RemoveRepresentativeCommand(supplierId, representativeId);
    }
}
