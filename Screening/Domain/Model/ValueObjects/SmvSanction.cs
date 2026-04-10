namespace DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

public record SmvSanction(
    string Date,
    string Resolution,
    string Summary,
    string Type,
    string Amount,
    string WithAppeal,
    string ResolutiveResolutionNumber,
    string ResolutiveResolutionDate);
