namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

public record SmvScreeningHitResponse(
    string Date,
    string Resolution,
    string Summary,
    string Type,
    string Amount,
    string WithAppeal,
    string ResolutiveResolutionNumber,
    string ResolutiveResolutionDate);

