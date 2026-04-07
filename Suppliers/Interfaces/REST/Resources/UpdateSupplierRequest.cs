namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

public record UpdateSupplierRequest(
    string CorporateName,
    string TradeName,
    string PhoneNumber,
    string Email,
    string WebSite,
    string PhysicalAddress,
    string Country,
    decimal AnnualBillingAmount);
