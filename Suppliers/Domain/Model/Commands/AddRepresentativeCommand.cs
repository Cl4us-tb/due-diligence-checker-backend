namespace DueDiligenceChecker.Suppliers.Domain.Model.Commands;

public record AddRepresentativeCommand(
    int SupplierId,
    string Role,
    string FirstName,
    string LastName,
    int? Age,
    string? Nationality);
