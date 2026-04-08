using DueDiligenceChecker.Screening.Domain.Model.Queries;
using DueDiligenceChecker.Screening.Interfaces.ACL;
using DueDiligenceChecker.Screening.Interfaces.REST.Resources;
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
    public async Task<IActionResult> Smv([FromQuery] string entityName, CancellationToken cancellationToken)
    {
        var items = await _facade.CheckSmvAsync(new SmvSanctionsByEntityNameQuery(entityName), cancellationToken);
        return Ok(new SmvScreeningResponse(items.Count, items));
    }

    [HttpGet("secop")]
    public async Task<IActionResult> Secop([FromQuery] string contractorName, CancellationToken cancellationToken)
    {
        var items = await _facade.CheckSecopAsync(new SecopSanctionsByContractorNameQuery(contractorName), cancellationToken);
        return Ok(new SecopScreeningResponse(items.Count, items));
    }

    [HttpGet("interpol/red")]
    public async Task<IActionResult> InterpolRed(
        [FromQuery] string familyName,
        [FromQuery] string forename,
        [FromQuery] string? nationality,
        [FromQuery] int? age,
        [FromQuery] string? gender,
        CancellationToken cancellationToken)
    {
        var query = new InterpolRedNoticesQuery(familyName, forename, nationality, age, gender);
        var items = await _facade.CheckInterpolAsync(query, cancellationToken);
        return Ok(new InterpolScreeningResponse(items.Count, items));
    }
}
