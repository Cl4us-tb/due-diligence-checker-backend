using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Application.OutboundServices;

public interface ISecopScraper
{
    Task<IReadOnlyList<SecopSanction>> SearchSanctionsAsync(
        SecopSanctionsByContractorNameQuery query,
        CancellationToken cancellationToken = default);
}
