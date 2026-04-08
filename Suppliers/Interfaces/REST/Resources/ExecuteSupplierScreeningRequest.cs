using DueDiligenceChecker.Suppliers.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;

public record ExecuteSupplierScreeningRequest(List<ScreeningSource> Sources);

