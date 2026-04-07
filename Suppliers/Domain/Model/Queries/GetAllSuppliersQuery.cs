namespace DueDiligenceChecker.Suppliers.Domain.Model.Queries;

public record GetAllSuppliersQuery(int Page, int Limit, string? SearchTerm);
