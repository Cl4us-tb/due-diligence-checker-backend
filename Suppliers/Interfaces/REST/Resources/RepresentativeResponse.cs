namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

public record RepresentativeResponse(
    int RepresentativeId,
    string Role,
    string FirstName,
    string LastName,
    int? Age,
    string? Nationality);
