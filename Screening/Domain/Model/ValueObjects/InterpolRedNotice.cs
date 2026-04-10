namespace DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

public record InterpolRedNotice(
    string FamilyName,
    string Forename,
    string Gender,
    DateOnly? DateOfBirth,
    string PlaceOfBirth,
    string Nationality,
    string Charges);
