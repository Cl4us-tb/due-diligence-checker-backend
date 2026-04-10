using DueDiligenceChecker.Suppliers.Domain.Model.ValueObjects;
using System.Linq;

namespace DueDiligenceChecker.Suppliers.Domain.Model.Entities;

public class Supplier
{
    public int SupplierId { get; private set; }
    public string CorporateName { get; private set; }
    public string TradeName { get; private set; }
    public TaxIdentification TaxId { get; private set; }
    public string PhoneNumber { get; private set; }
    public string Email { get; private set; }
    public string WebSite { get; private set; }
    public string PhysicalAddress { get; private set; }
    public string Country { get; private set; }
    public AnnualBilling Billing { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public int CreatedBy { get; private set; }
    public List<Representative> Representatives { get; private set; }

    protected Supplier()
    {
        CorporateName = null!;
        TradeName = null!;
        TaxId = null!;
        PhoneNumber = null!;
        Email = null!;
        WebSite = null!;
        PhysicalAddress = null!;
        Country = null!;
        Billing = null!;
        Representatives = new List<Representative>();
    }

    public Supplier(
        string corporateName,
        string tradeName,
        TaxIdentification taxId,
        string phoneNumber,
        string email,
        string webSite,
        string physicalAddress,
        string country,
        AnnualBilling billing,
        int createdBy)
    {
        CorporateName = RequireNonEmpty(corporateName, nameof(corporateName));
        TradeName = RequireNonEmpty(tradeName, nameof(tradeName));
        TaxId = taxId ?? throw new ArgumentNullException(nameof(taxId));
        PhoneNumber = RequireNonEmpty(phoneNumber, nameof(phoneNumber));
        Email = RequireNonEmpty(email, nameof(email));
        WebSite = RequireNonEmpty(webSite, nameof(webSite));
        PhysicalAddress = RequireNonEmpty(physicalAddress, nameof(physicalAddress));
        Country = RequireNonEmpty(country, nameof(country));
        Billing = billing ?? throw new ArgumentNullException(nameof(billing));

        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
        CreatedBy = createdBy;
        Representatives = new List<Representative>();
    }

    public void Update(
        string corporateName,
        string tradeName,
        string phoneNumber,
        string email,
        string webSite,
        string physicalAddress,
        string country,
        AnnualBilling billing)
    {
        CorporateName = RequireNonEmpty(corporateName, nameof(corporateName));
        TradeName = RequireNonEmpty(tradeName, nameof(tradeName));
        PhoneNumber = RequireNonEmpty(phoneNumber, nameof(phoneNumber));
        Email = RequireNonEmpty(email, nameof(email));
        WebSite = RequireNonEmpty(webSite, nameof(webSite));
        PhysicalAddress = RequireNonEmpty(physicalAddress, nameof(physicalAddress));
        Country = RequireNonEmpty(country, nameof(country));
        Billing = billing ?? throw new ArgumentNullException(nameof(billing));

        UpdatedAt = DateTime.UtcNow;
    }

    public void AddRepresentative(string role, string firstName, string lastName, int? age, string? nationality)
    {
        Representatives.Add(new Representative(role, firstName, lastName, age, nationality));
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateRepresentative(int representativeId, string role, string firstName, string lastName, int? age, string? nationality)
    {
        var representative = Representatives.FirstOrDefault(r => r.RepresentativeId == representativeId);
        if (representative == null) throw new ArgumentException("Representante no encontrado en este proveedor.");
        representative.Update(role, firstName, lastName, age, nationality);
        UpdatedAt = DateTime.UtcNow;
    }

    public void RemoveRepresentative(int representativeId)
    {
        var representative = Representatives.FirstOrDefault(r => r.RepresentativeId == representativeId);
        if (representative == null) throw new ArgumentException("Representante no encontrado en este proveedor.");
        Representatives.Remove(representative);
        UpdatedAt = DateTime.UtcNow;
    }

    private static string RequireNonEmpty(string value, string paramName)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("El valor no puede estar vacío.", paramName);

        return value;
    }
}
