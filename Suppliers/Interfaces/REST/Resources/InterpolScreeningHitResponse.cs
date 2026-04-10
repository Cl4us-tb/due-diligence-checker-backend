namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

public record InterpolScreeningHitResponse(
    string FamilyName,
    string Forename,
    string Gender,
    DateOnly? DateOfBirth,
    string PlaceOfBirth,
    string Nationality,
    string Charges);

