using DueDiligenceChecker.Suppliers.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Suppliers.Domain.Model.Commands;

public record ExecuteSupplierScreeningCommand(int SupplierId, List<ScreeningSource> Sources);
