using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Afraz.Api;

internal sealed class GlobalExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception for trace {TraceId}", httpContext.TraceIdentifier);

        var statusCode = exception is ValidationException
            ? StatusCodes.Status400BadRequest
            : StatusCodes.Status500InternalServerError;

        httpContext.Response.StatusCode = statusCode;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = statusCode == StatusCodes.Status400BadRequest
                    ? "Validation failed"
                    : "An unexpected error occurred",
                Detail = statusCode == StatusCodes.Status400BadRequest ? exception.Message : null,
            },
            Exception = exception,
        });
    }
}

