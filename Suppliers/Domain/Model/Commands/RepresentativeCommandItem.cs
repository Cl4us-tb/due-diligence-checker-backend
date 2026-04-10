namespace DueDiligenceChecker.Suppliers.Domain.Model.Commands;

public record RepresentativeCommandItem(string Role, string FirstName, string LastName, int? Age, string? Nationality);
