namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

public record RepresentativeRequestItem(string Role, string FirstName, string LastName, int? Age, string? Nationality);
