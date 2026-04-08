using DueDiligenceChecker.Suppliers.Domain.Model.Commands;

namespace DueDiligenceChecker.Suppliers.Application.InboundServices;

public interface ISupplierScreeningCommandService
{
    Task<int> Handle(ExecuteSupplierScreeningCommand command, CancellationToken cancellationToken = default);
}
