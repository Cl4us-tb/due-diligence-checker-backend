namespace DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;

public class SmvScreeningHit
{
    public int SmvScreeningHitId { get; private set; }
    public string Date { get; private set; }
    public string Resolution { get; private set; }
    public string Summary { get; private set; }
    public string Type { get; private set; }
    public string Amount { get; private set; }
    public string WithAppeal { get; private set; }
    public string ResolutiveResolutionNumber { get; private set; }
    public string ResolutiveResolutionDate { get; private set; }

    protected SmvScreeningHit()
    {
        Date = null!;
        Resolution = null!;
        Summary = null!;
        Type = null!;
        Amount = null!;
        WithAppeal = null!;
        ResolutiveResolutionNumber = null!;
        ResolutiveResolutionDate = null!;
    }

    public SmvScreeningHit(
        string date,
        string resolution,
        string summary,
        string type,
        string amount,
        string withAppeal,
        string resolutiveResolutionNumber,
        string resolutiveResolutionDate)
    {
        Date = date;
        Resolution = resolution;
        Summary = summary;
        Type = type;
        Amount = amount;
        WithAppeal = withAppeal;
        ResolutiveResolutionNumber = resolutiveResolutionNumber;
        ResolutiveResolutionDate = resolutiveResolutionDate;
    }
}
