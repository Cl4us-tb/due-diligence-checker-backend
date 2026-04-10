using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DueDiligenceChecker.Shared.Infrastructure.Errors;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception exception)
    {
        var (statusCode, title, detail) = MapException(exception);

        if (statusCode >= 500)
            _logger.LogError(exception, "Unhandled exception");
        else
            _logger.LogInformation(exception, "Handled exception");

        context.Response.Clear();
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/problem+json";

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };

        problem.Extensions["traceId"] = context.TraceIdentifier;

        if (exception is ArgumentException argumentException && !string.IsNullOrWhiteSpace(argumentException.ParamName))
            problem.Extensions["paramName"] = argumentException.ParamName;

        await context.Response.WriteAsJsonAsync(problem);
    }

    private static (int statusCode, string title, string detail) MapException(Exception exception)
    {
        switch (exception)
        {
            case UnauthorizedAccessException:
                return (StatusCodes.Status401Unauthorized, "No autorizado", exception.Message);

            case ArgumentException:
                return (StatusCodes.Status400BadRequest, "Solicitud inválida", exception.Message);

            case InvalidOperationException:
                return (StatusCodes.Status409Conflict, "Conflicto", exception.Message);

            case KeyNotFoundException:
                return (StatusCodes.Status404NotFound, "No encontrado", exception.Message);

            case DbUpdateException dbUpdateException when IsUniqueConstraintViolation(dbUpdateException):
                return (StatusCodes.Status409Conflict, "Conflicto", "Ya existe un registro con el mismo identificador único.");

            default:
                return (StatusCodes.Status500InternalServerError, "Error interno", "Ocurrió un error inesperado.");
        }
    }

    private static bool IsUniqueConstraintViolation(DbUpdateException exception)
    {
        if (exception.InnerException is not SqlException sqlException) return false;
        return sqlException.Number is 2601 or 2627;
    }
}
