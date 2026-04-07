namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

public record AddRepresentativeRequest(string Role, string FirstName, string LastName, int? Age, string? Nationality);
