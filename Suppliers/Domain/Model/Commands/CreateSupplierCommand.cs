namespace DueDiligenceChecker.Suppliers.Domain.Model.Commands;

public record CreateSupplierCommand(
    string CorporateName,
    string TradeName,
    string TaxIdString,
    string PhoneNumber,
    string Email,
    string WebSite,
    string PhysicalAddress,
    string Country,
    decimal AnnualBillingAmount,
    int CreatedBy,
    List<RepresentativeCommandItem>? Representatives);
