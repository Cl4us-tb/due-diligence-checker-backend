namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

public record UpdateRepresentativeRequest(string Role, string FirstName, string LastName, int? Age, string? Nationality);
