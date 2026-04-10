namespace DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;

public class SecopScreeningHit
{
    public int SecopScreeningHitId { get; private set; }
    public string EntityName { get; private set; }
    public string EntityTaxId { get; private set; }
    public string Level { get; private set; }
    public string Order { get; private set; }
    public string Municipality { get; private set; }
    public string ResolutionNumber { get; private set; }
    public string ContractorDocument { get; private set; }
    public string ContractorName { get; private set; }
    public string ContractNumber { get; private set; }
    public decimal? SanctionAmount { get; private set; }
    public DateTime? PublishedAt { get; private set; }
    public DateTime? FinalizedAt { get; private set; }
    public DateTime? LoadedAt { get; private set; }
    public string ProcessUrl { get; private set; }

    protected SecopScreeningHit()
    {
        EntityName = null!;
        EntityTaxId = null!;
        Level = null!;
        Order = null!;
        Municipality = null!;
        ResolutionNumber = null!;
        ContractorDocument = null!;
        ContractorName = null!;
        ContractNumber = null!;
        ProcessUrl = null!;
    }

    public SecopScreeningHit(
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
        EntityName = entityName;
        EntityTaxId = entityTaxId;
        Level = level;
        Order = order;
        Municipality = municipality;
        ResolutionNumber = resolutionNumber;
        ContractorDocument = contractorDocument;
        ContractorName = contractorName;
        ContractNumber = contractNumber;
        SanctionAmount = sanctionAmount;
        PublishedAt = publishedAt;
        FinalizedAt = finalizedAt;
        LoadedAt = loadedAt;
        ProcessUrl = processUrl;
    }
}
