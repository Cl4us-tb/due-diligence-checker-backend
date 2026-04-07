using DueDiligenceChecker.Shared.Domain.Repositories;
using DueDiligenceChecker.Suppliers.Application.InboundServices;
using DueDiligenceChecker.Suppliers.Domain.Model.Commands;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities;
using DueDiligenceChecker.Suppliers.Domain.Model.ValueObjects;
using DueDiligenceChecker.Suppliers.Domain.Repositories;

namespace DueDiligenceChecker.Suppliers.Application.Internal.CommandServices;

public class SupplierCommandService : ISupplierCommandService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly IUnitOfWork _unitOfWork;

    public SupplierCommandService(ISupplierRepository supplierRepository, IUnitOfWork unitOfWork)
    {
        _supplierRepository = supplierRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(CreateSupplierCommand command)
    {
        if (await _supplierRepository.ExistsByTaxIdAsync(command.TaxIdString))
            throw new InvalidOperationException("Ya existe un proveedor con la misma identificación tributaria.");

        var supplier = new Supplier(
            command.CorporateName,
            command.TradeName,
            new TaxIdentification(command.TaxIdString),
            command.PhoneNumber,
            command.Email,
            command.WebSite,
            command.PhysicalAddress,
            command.Country,
            new AnnualBilling(command.AnnualBillingAmount),
            command.CreatedBy);

        if (command.Representatives != null)
        {
            foreach (var representative in command.Representatives)
            {
                supplier.AddRepresentative(
                    representative.Role,
                    representative.FirstName,
                    representative.LastName,
                    representative.Age,
                    representative.Nationality);
            }
        }

        await _supplierRepository.AddAsync(supplier);
        await _unitOfWork.CompleteAsync();

        return supplier.SupplierId;
    }

    public async Task Handle(UpdateSupplierCommand command)
    {
        var supplier = await _supplierRepository.FindByIdAsync(command.SupplierId);
        if (supplier == null) throw new ArgumentException("Proveedor no encontrado.");

        supplier.Update(
            command.CorporateName,
            command.TradeName,
            command.PhoneNumber,
            command.Email,
            command.WebSite,
            command.PhysicalAddress,
            command.Country,
            new AnnualBilling(command.AnnualBillingAmount));

        _supplierRepository.Update(supplier);
        await _unitOfWork.CompleteAsync();
    }

    public async Task Handle(DeleteSupplierCommand command)
    {
        var supplier = await _supplierRepository.FindByIdAsync(command.SupplierId);
        if (supplier == null) throw new ArgumentException("Proveedor no encontrado.");

        _supplierRepository.Remove(supplier);
        await _unitOfWork.CompleteAsync();
    }

    public async Task Handle(AddRepresentativeCommand command)
    {
        var supplier = await _supplierRepository.FindByIdAsync(command.SupplierId);
        if (supplier == null) throw new ArgumentException("Proveedor no encontrado.");

        supplier.AddRepresentative(command.Role, command.FirstName, command.LastName, command.Age, command.Nationality);

        _supplierRepository.Update(supplier);
        await _unitOfWork.CompleteAsync();
    }

    public async Task Handle(UpdateRepresentativeCommand command)
    {
        var supplier = await _supplierRepository.FindByIdAsync(command.SupplierId);
        if (supplier == null) throw new ArgumentException("Proveedor no encontrado.");

        supplier.UpdateRepresentative(
            command.RepresentativeId,
            command.Role,
            command.FirstName,
            command.LastName,
            command.Age,
            command.Nationality);

        _supplierRepository.Update(supplier);
        await _unitOfWork.CompleteAsync();
    }

    public async Task Handle(RemoveRepresentativeCommand command)
    {
        var supplier = await _supplierRepository.FindByIdAsync(command.SupplierId);
        if (supplier == null) throw new ArgumentException("Proveedor no encontrado.");

        supplier.RemoveRepresentative(command.RepresentativeId);

        _supplierRepository.Update(supplier);
        await _unitOfWork.CompleteAsync();
    }
}
