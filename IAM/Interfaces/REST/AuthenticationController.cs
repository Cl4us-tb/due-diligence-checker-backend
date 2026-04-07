using DueDiligenceChecker.IAM.Application.InboundServices;
using DueDiligenceChecker.IAM.Interfaces.REST.Resources;
using DueDiligenceChecker.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;

namespace DueDiligenceChecker.IAM.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IUserCommandService _userCommandService;
    private readonly IUserQueryService _userQueryService;

    public AuthenticationController(IUserCommandService userCommandService, IUserQueryService userQueryService)
    {
        _userCommandService = userCommandService;
        _userQueryService = userQueryService;
    }

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
    {
        var command = SignUpCommandFromResourceAssembler.ToCommandFromResource(request);

        try
        {
            await _userCommandService.Handle(command);
            
            return Ok(new { message = "Usuario registrado exitosamente." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
    {
        var query = SignInQueryFromRequestAssembler.ToQuery(request);

        var auth = await _userQueryService.AuthenticateAsync(query);

        if (auth == null)
        {
            return Unauthorized(new { message = "Credenciales invÃ¡lidas." });
        }

        var (user, token) = auth.Value;

        var response = SignInQueryFromRequestAssembler.ToResponse(user, token);

        return Ok(response);
    }
}






