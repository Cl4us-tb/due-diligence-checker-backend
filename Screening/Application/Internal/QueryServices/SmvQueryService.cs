using DueDiligenceChecker.Screening.Application.InboundServices;
using DueDiligenceChecker.Screening.Application.OutboundServices;
using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Application.Internal.QueryServices;

public class SmvQueryService : ISmvQueryService
{
    private readonly ISmvScraper _scraper;

    public SmvQueryService(ISmvScraper scraper)
    {
        _scraper = scraper;
    }

    public async Task<IReadOnlyList<SmvSanction>> Handle(
        SmvSanctionsByEntityNameQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _scraper.SearchSanctionsAsync(query, cancellationToken);
    }
}
