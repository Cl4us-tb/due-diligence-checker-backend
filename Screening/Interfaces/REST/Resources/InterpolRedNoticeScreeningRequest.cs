namespace DueDiligenceChecker.Screening.Interfaces.REST.Resources;

public record InterpolRedNoticeScreeningRequest(
    string FamilyName,
    string Forename,
    string? Nationality = null,
    int? Age = null,
    string? Gender = null);
