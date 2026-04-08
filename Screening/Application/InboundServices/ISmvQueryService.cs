using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Application.InboundServices;

public interface ISmvQueryService
{
    Task<IReadOnlyList<SmvSanction>> Handle(
        SmvSanctionsByEntityNameQuery query,
        CancellationToken cancellationToken = default);
}
