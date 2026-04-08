using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Application.OutboundServices;

public interface IInterpolScraper
{
    Task<IReadOnlyList<InterpolRedNotice>> SearchRedNoticesAsync(
        InterpolRedNoticesQuery query,
        CancellationToken cancellationToken = default);
}
