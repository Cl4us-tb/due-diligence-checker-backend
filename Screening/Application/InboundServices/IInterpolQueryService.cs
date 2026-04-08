using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Application.InboundServices;

public interface IInterpolQueryService
{
    Task<IReadOnlyList<InterpolRedNotice>> Handle(
        InterpolRedNoticesQuery query,
        CancellationToken cancellationToken = default);
}
