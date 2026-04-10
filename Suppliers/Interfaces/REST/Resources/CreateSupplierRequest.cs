namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

public record CreateSupplierRequest(
    string CorporateName,
    string TradeName,
    string TaxIdentification,
    string PhoneNumber,
    string Email,
    string WebSite,
    string PhysicalAddress,
    string Country,
    decimal AnnualBillingAmount,
    List<RepresentativeRequestItem>? Representatives);
