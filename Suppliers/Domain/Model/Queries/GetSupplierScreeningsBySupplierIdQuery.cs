using DueDiligenceChecker.Suppliers.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Suppliers.Domain.Model.Queries;

public record GetSupplierScreeningsBySupplierIdQuery(
    int SupplierId,
    int Page = 1,
    int Limit = 10,
    ScreeningSource? Source = null);
