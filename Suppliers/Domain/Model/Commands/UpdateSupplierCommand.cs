namespace DueDiligenceChecker.Suppliers.Domain.Model.Commands;

public record UpdateSupplierCommand(
    int SupplierId,
    string CorporateName,
    string TradeName,
    string PhoneNumber,
    string Email,
    string WebSite,
    string PhysicalAddress,
    string Country,
    decimal AnnualBillingAmount);
