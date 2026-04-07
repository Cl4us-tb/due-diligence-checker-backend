namespace DueDiligenceChecker.Suppliers.Domain.Model.Commands;

public record UpdateRepresentativeCommand(
    int SupplierId,
    int RepresentativeId,
    string Role,
    string FirstName,
    string LastName,
    int? Age,
    string? Nationality);
