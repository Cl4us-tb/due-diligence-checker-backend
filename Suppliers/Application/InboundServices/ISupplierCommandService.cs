using DueDiligenceChecker.Suppliers.Domain.Model.Commands;

namespace DueDiligenceChecker.Suppliers.Application.InboundServices;

public interface ISupplierCommandService
{
    Task<int> Handle(CreateSupplierCommand command);
    Task Handle(UpdateSupplierCommand command);
    Task Handle(DeleteSupplierCommand command);
    Task Handle(AddRepresentativeCommand command);
    Task Handle(UpdateRepresentativeCommand command);
    Task Handle(RemoveRepresentativeCommand command);
}
