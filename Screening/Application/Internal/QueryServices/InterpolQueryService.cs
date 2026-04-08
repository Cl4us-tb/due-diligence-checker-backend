using DueDiligenceChecker.Screening.Application.InboundServices;
using DueDiligenceChecker.Screening.Application.OutboundServices;
using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Application.Internal.QueryServices;

public class InterpolQueryService : IInterpolQueryService
{
    private readonly IInterpolScraper _scraper;

    public InterpolQueryService(IInterpolScraper scraper)
    {
        _scraper = scraper;
    }

    public async Task<IReadOnlyList<InterpolRedNotice>> Handle(
        InterpolRedNoticesQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _scraper.SearchRedNoticesAsync(query, cancellationToken);
    }
}
