using DueDiligenceChecker.Suppliers.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;

public class SupplierScreening
{
    public int SupplierScreeningId { get; private set; }
    public int SupplierId { get; private set; }
    public DateTime ExecutedAt { get; private set; }
    public bool HasHits { get; private set; }
    public string SourcesChecked { get; private set; }

    public List<InterpolScreeningHit> InterpolHits { get; private set; }
    public List<SecopScreeningHit> SecopHits { get; private set; }
    public List<SmvScreeningHit> SmvHits { get; private set; }

    protected SupplierScreening()
    {
        SourcesChecked = null!;
        InterpolHits = new List<InterpolScreeningHit>();
        SecopHits = new List<SecopScreeningHit>();
        SmvHits = new List<SmvScreeningHit>();
    }

    public SupplierScreening(int supplierId, List<ScreeningSource> sources)
    {
        SupplierId = supplierId;
        ExecutedAt = DateTime.UtcNow;
        SourcesChecked = string.Join(", ", sources);
        HasHits = false;
        InterpolHits = new List<InterpolScreeningHit>();
        SecopHits = new List<SecopScreeningHit>();
        SmvHits = new List<SmvScreeningHit>();
    }

    public void AddInterpolHit(
        string familyName,
        string forename,
        string gender,
        DateOnly? dateOfBirth,
        string placeOfBirth,
        string nationality,
        string charges)
    {
        InterpolHits.Add(new InterpolScreeningHit(familyName, forename, gender, dateOfBirth, placeOfBirth, nationality, charges));
        HasHits = true;
    }

    public void AddSecopHit(
        string entityName,
        string entityTaxId,
        string level,
        string order,
        string municipality,
        string resolutionNumber,
        string contractorDocument,
        string contractorName,
        string contractNumber,
        decimal? sanctionAmount,
        DateTime? publishedAt,
        DateTime? finalizedAt,
        DateTime? loadedAt,
        string processUrl)
    {
        SecopHits.Add(new SecopScreeningHit(
            entityName,
            entityTaxId,
            level,
            order,
            municipality,
            resolutionNumber,
            contractorDocument,
            contractorName,
            contractNumber,
            sanctionAmount,
            publishedAt,
            finalizedAt,
            loadedAt,
            processUrl));
        HasHits = true;
    }

    public void AddSmvHit(
        string date,
        string resolution,
        string summary,
        string type,
        string amount,
        string withAppeal,
        string resolutiveResolutionNumber,
        string resolutiveResolutionDate)
    {
        SmvHits.Add(new SmvScreeningHit(date, resolution, summary, type, amount, withAppeal, resolutiveResolutionNumber, resolutiveResolutionDate));
        HasHits = true;
    }
}
