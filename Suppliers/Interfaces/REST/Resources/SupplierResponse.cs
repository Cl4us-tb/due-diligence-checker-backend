namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

public record SupplierResponse(
    int SupplierId,
    string CorporateName,
    string TradeName,
    string TaxIdentification,
    string PhoneNumber,
    string Email,
    string WebSite,
    string PhysicalAddress,
    string Country,
    decimal AnnualBillingAmount,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int CreatedBy,
    List<RepresentativeResponse> Representatives);
