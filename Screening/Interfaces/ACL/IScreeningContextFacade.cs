using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Interfaces.ACL;

public interface IScreeningContextFacade
{
    Task<IReadOnlyList<SecopSanction>> CheckSecopAsync(
        SecopSanctionsByContractorNameQuery query,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<InterpolRedNotice>> CheckInterpolAsync(
        InterpolRedNoticesQuery query,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<SmvSanction>> CheckSmvAsync(
        SmvSanctionsByEntityNameQuery query,
        CancellationToken cancellationToken = default);
}
