using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Application.InboundServices;

public interface ISecopQueryService
{
    Task<IReadOnlyList<SecopSanction>> Handle(
        SecopSanctionsByContractorNameQuery query,
        CancellationToken cancellationToken = default);
}
