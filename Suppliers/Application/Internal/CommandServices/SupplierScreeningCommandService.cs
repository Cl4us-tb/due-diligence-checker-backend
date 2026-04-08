using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Interfaces.ACL;
using DueDiligenceChecker.Shared.Domain.Repositories;
using DueDiligenceChecker.Suppliers.Application.InboundServices;
using DueDiligenceChecker.Suppliers.Domain.Model.Commands;
using DueDiligenceChecker.Suppliers.Domain.Model.Entities.History;
using DueDiligenceChecker.Suppliers.Domain.Model.ValueObjects;
using DueDiligenceChecker.Suppliers.Domain.Repositories;

namespace DueDiligenceChecker.Suppliers.Application.Internal.CommandServices;

public class SupplierScreeningCommandService : ISupplierScreeningCommandService
{
    private readonly ISupplierRepository _supplierRepository;
    private readonly ISupplierScreeningRepository _supplierScreeningRepository;
    private readonly IScreeningContextFacade _screeningContextFacade;
    private readonly IUnitOfWork _unitOfWork;

    public SupplierScreeningCommandService(
        ISupplierRepository supplierRepository,
        ISupplierScreeningRepository supplierScreeningRepository,
        IScreeningContextFacade screeningContextFacade,
        IUnitOfWork unitOfWork)
    {
        _supplierRepository = supplierRepository;
        _supplierScreeningRepository = supplierScreeningRepository;
        _screeningContextFacade = screeningContextFacade;
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(ExecuteSupplierScreeningCommand command, CancellationToken cancellationToken = default)
    {
        if (command.Sources == null || command.Sources.Count == 0)
            throw new ArgumentException("Debe seleccionar al menos una fuente para ejecutar el screening.");

        var supplier = await _supplierRepository.FindByIdAsync(command.SupplierId);
        if (supplier == null) throw new ArgumentException("Proveedor no encontrado.");

        var screening = new SupplierScreening(command.SupplierId, command.Sources);

        if (command.Sources.Contains(ScreeningSource.Secop))
        {
            var secopItems = await _screeningContextFacade.CheckSecopAsync(
                new SecopSanctionsByContractorNameQuery(supplier.CorporateName),
                cancellationToken);

            foreach (var item in secopItems)
            {
                screening.AddSecopHit(
                    item.EntityName,
                    item.EntityTaxId,
                    item.Level,
                    item.Order,
                    item.Municipality,
                    item.ResolutionNumber,
                    item.ContractorDocument,
                    item.ContractorName,
                    item.ContractNumber,
                    item.SanctionAmount,
                    item.PublishedAt,
                    item.FinalizedAt,
                    item.LoadedAt,
                    item.ProcessUrl);
            }
        }

        if (command.Sources.Contains(ScreeningSource.Interpol))
        {
            foreach (var representative in supplier.Representatives)
            {
                var interpolItems = await _screeningContextFacade.CheckInterpolAsync(
                    new InterpolRedNoticesQuery(representative.LastName, representative.FirstName),
                    cancellationToken);

                foreach (var item in interpolItems)
                {
                    screening.AddInterpolHit(
                        item.FamilyName,
                        item.Forename,
                        item.Gender,
                        item.DateOfBirth,
                        item.PlaceOfBirth,
                        item.Nationality,
                        item.Charges);
                }
            }
        }

        if (command.Sources.Contains(ScreeningSource.Smv))
        {
            var smvItems = await _screeningContextFacade.CheckSmvAsync(
                new SmvSanctionsByEntityNameQuery(supplier.CorporateName),
                cancellationToken);

            foreach (var item in smvItems)
            {
                screening.AddSmvHit(
                    item.Date,
                    item.Resolution,
                    item.Summary,
                    item.Type,
                    item.Amount,
                    item.WithAppeal,
                    item.ResolutiveResolutionNumber,
                    item.ResolutiveResolutionDate);
            }
        }

        await _supplierScreeningRepository.AddAsync(screening);
        await _unitOfWork.CompleteAsync();

        return screening.SupplierScreeningId;
    }
}
