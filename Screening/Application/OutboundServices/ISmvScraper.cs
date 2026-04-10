using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Application.OutboundServices;

public interface ISmvScraper
{
    Task<IReadOnlyList<SmvSanction>> SearchSanctionsAsync(
        SmvSanctionsByEntityNameQuery query,
        CancellationToken cancellationToken = default);
}
