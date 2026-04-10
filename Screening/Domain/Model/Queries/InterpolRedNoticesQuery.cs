namespace DueDiligenceChecker.Screening.Domain.Model.Queries;

public record InterpolRedNoticesQuery(
    string FamilyName,
    string Forename,
    string? Nationality = null,
    int? Age = null,
    string? Gender = null);
