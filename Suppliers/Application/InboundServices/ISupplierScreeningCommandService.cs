using DueDiligenceChecker.Suppliers.Domain.Model.Commands;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;

namespace DueDiligenceChecker.Suppliers.Application.InboundServices;

public interface ISupplierScreeningCommandService
{
    Task<SupplierScreening> Handle(ExecuteSupplierScreeningCommand command, CancellationToken cancellationToken = default);
}
