using DueDiligenceChecker.Screening.Application.InboundServices;
using DueDiligenceChecker.Screening.Application.OutboundServices;
using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Application.Internal.QueryServices;

public class SecopQueryService : ISecopQueryService
{
    private readonly ISecopScraper _scraper;

    public SecopQueryService(ISecopScraper scraper)
    {
        _scraper = scraper;
    }

    public async Task<IReadOnlyList<SecopSanction>> Handle(
        SecopSanctionsByContractorNameQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _scraper.SearchSanctionsAsync(query, cancellationToken);
    }
}
