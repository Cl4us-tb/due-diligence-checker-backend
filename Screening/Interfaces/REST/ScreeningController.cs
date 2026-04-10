using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Interfaces.ACL;
using DueDiligenceChecker.Screening.Interfaces.REST.Resources;
using DueDiligenceChecker.Screening.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DueDiligenceChecker.Screening.Interfaces.REST;

[ApiController]
[Route("api/v1/screening")]
[Authorize]
public class ScreeningController : ControllerBase
{
    private readonly IScreeningContextFacade _facade;

    public ScreeningController(IScreeningContextFacade facade)
    {
        _facade = facade;
    }

    [HttpGet("smv")]
    public async Task<IActionResult> Smv([FromQuery] SmvScreeningRequest request, CancellationToken cancellationToken)
    {
        var query = ScreeningQueryFromRequestAssembler.ToQuery(request);
        var items = await _facade.CheckSmvAsync(query, cancellationToken);
        return Ok(new SmvScreeningResponse(items.Count, items));
    }

    [HttpGet("secop")]
    public async Task<IActionResult> Secop([FromQuery] SecopScreeningRequest request, CancellationToken cancellationToken)
    {
        var query = ScreeningQueryFromRequestAssembler.ToQuery(request);
        var items = await _facade.CheckSecopAsync(query, cancellationToken);
        return Ok(new SecopScreeningResponse(items.Count, items));
    }

    [HttpGet("interpol/red")]
    public async Task<IActionResult> InterpolRed(
        [FromQuery] InterpolRedNoticeScreeningRequest request,
        CancellationToken cancellationToken)
    {
        var query = ScreeningQueryFromRequestAssembler.ToQuery(request);
        var items = await _facade.CheckInterpolAsync(query, cancellationToken);
        return Ok(new InterpolScreeningResponse(items.Count, items));
    }
}
