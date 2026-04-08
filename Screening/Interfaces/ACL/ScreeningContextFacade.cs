using DueDiligenceChecker.Screening.Application.InboundServices;
using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Domain.Model.ValueObjects;

namespace DueDiligenceChecker.Screening.Interfaces.ACL;

public class ScreeningContextFacade : IScreeningContextFacade
{
    private readonly ISecopQueryService _secopQueryService;
    private readonly IInterpolQueryService _interpolQueryService;
    private readonly ISmvQueryService _smvQueryService;

    public ScreeningContextFacade(
        ISecopQueryService secopQueryService,
        IInterpolQueryService interpolQueryService,
        ISmvQueryService smvQueryService)
    {
        _secopQueryService = secopQueryService;
        _interpolQueryService = interpolQueryService;
        _smvQueryService = smvQueryService;
    }

    public async Task<IReadOnlyList<SecopSanction>> CheckSecopAsync(
        SecopSanctionsByContractorNameQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _secopQueryService.Handle(query, cancellationToken);
    }

    public async Task<IReadOnlyList<InterpolRedNotice>> CheckInterpolAsync(
        InterpolRedNoticesQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _interpolQueryService.Handle(query, cancellationToken);
    }

    public async Task<IReadOnlyList<SmvSanction>> CheckSmvAsync(
        SmvSanctionsByEntityNameQuery query,
        CancellationToken cancellationToken = default)
    {
        return await _smvQueryService.Handle(query, cancellationToken);
    }
}
