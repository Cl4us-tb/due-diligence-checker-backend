using DueDiligenceChecker.Suppliers.Application.InboundServices;
using DueDiligenceChecker.Suppliers.Interfaces.REST.Resources;
using DueDiligenceChecker.Suppliers.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DueDiligenceChecker.Suppliers.Interfaces.REST;

[ApiController]
[Route("api/v1/suppliers")]
[Authorize]
public class SuppliersController : ControllerBase
{
    private readonly ISupplierCommandService _commandService;
    private readonly ISupplierQueryService _queryService;
    private readonly ISupplierScreeningCommandService _screeningCommandService;

    public SuppliersController(
        ISupplierCommandService commandService,
        ISupplierQueryService queryService,
        ISupplierScreeningCommandService screeningCommandService)
    {
        _commandService = commandService;
        _queryService = queryService;
        _screeningCommandService = screeningCommandService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSupplierRequest request)
    {
        if (!TryGetUserId(out var userId)) return Unauthorized(new { message = "Token inválido o sin userId." });
        var command = SupplierCommandFromRequestAssembler.ToCommandFromRequest(request, userId);
        var supplierId = await _commandService.Handle(command);

        var supplier = await _queryService.Handle(SupplierQueryFromRequestAssembler.ToQuery(supplierId));
        if (supplier == null) return NotFound();

        var response = SupplierResponseFromEntityAssembler.ToResponseFromEntity(supplier);
        return CreatedAtAction(nameof(GetById), new { supplierId }, response);
    }

    [HttpPut("{supplierId:int}")]
    public async Task<IActionResult> Update([FromRoute] int supplierId, [FromBody] UpdateSupplierRequest request)
    {
        var command = SupplierCommandFromRequestAssembler.ToCommandFromRequest(supplierId, request);
        await _commandService.Handle(command);
        return await OkSupplierResponse(supplierId);
    }

    [HttpDelete("{supplierId:int}")]
    public async Task<IActionResult> Delete([FromRoute] int supplierId)
    {
        var command = SupplierCommandFromRequestAssembler.ToCommandFromRequest(supplierId);
        await _commandService.Handle(command);
        return Ok();
    }

    [HttpGet("{supplierId:int}")]
    public async Task<IActionResult> GetById([FromRoute] int supplierId)
    {
        var query = SupplierQueryFromRequestAssembler.ToQuery(supplierId);
        var supplier = await _queryService.Handle(query);
        if (supplier == null) return NotFound();
        return Ok(SupplierResponseFromEntityAssembler.ToResponseFromEntity(supplier));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int limit = 10, [FromQuery] string? searchTerm = null)
    {
        var query = SupplierQueryFromRequestAssembler.ToQuery(page, limit, searchTerm);
        var suppliers = await _queryService.Handle(query);
        var responses = suppliers.Select(SupplierResponseFromEntityAssembler.ToResponseFromEntity).ToList();
        return Ok(responses);
    }

    [HttpPost("{supplierId:int}/representatives")]
    public async Task<IActionResult> AddRepresentative([FromRoute] int supplierId, [FromBody] AddRepresentativeRequest request)
    {
        var command = SupplierCommandFromRequestAssembler.ToCommandFromRequest(supplierId, request);
        await _commandService.Handle(command);
        return await OkSupplierResponse(supplierId);
    }

    [HttpPut("{supplierId:int}/representatives/{representativeId:int}")]
    public async Task<IActionResult> UpdateRepresentative([FromRoute] int supplierId, [FromRoute] int representativeId, [FromBody] UpdateRepresentativeRequest request)
    {
        var command = SupplierCommandFromRequestAssembler.ToCommandFromRequest(supplierId, representativeId, request);
        await _commandService.Handle(command);
        return await OkSupplierResponse(supplierId);
    }

    [HttpDelete("{supplierId:int}/representatives/{representativeId:int}")]
    public async Task<IActionResult> RemoveRepresentative([FromRoute] int supplierId, [FromRoute] int representativeId)
    {
        var command = SupplierCommandFromRequestAssembler.ToCommandFromRequest(supplierId, representativeId);
        await _commandService.Handle(command);
        return await OkSupplierResponse(supplierId);
    }

    [HttpPost("{supplierId:int}/screenings")]
    public async Task<IActionResult> ExecuteScreening(
        [FromRoute] int supplierId,
        [FromBody] ExecuteSupplierScreeningRequest request,
        CancellationToken cancellationToken)
    {
        var command = SupplierScreeningCommandFromRequestAssembler.ToCommandFromRequest(supplierId, request);
        var screening = await _screeningCommandService.Handle(command, cancellationToken);
        var response = SupplierScreeningResponseFromEntityAssembler.ToResponseFromEntity(screening);
        return Ok(response);
    }

    private async Task<IActionResult> OkSupplierResponse(int supplierId)
    {
        var supplier = await _queryService.Handle(SupplierQueryFromRequestAssembler.ToQuery(supplierId));
        if (supplier == null) return NotFound();
        return Ok(SupplierResponseFromEntityAssembler.ToResponseFromEntity(supplier));
    }

    private bool TryGetUserId(out int userId)
    {
        var value = User.FindFirst("userId")?.Value;
        if (string.IsNullOrWhiteSpace(value))
        {
            userId = default;
            return false;
        }

        return int.TryParse(value, out userId);
    }
}
