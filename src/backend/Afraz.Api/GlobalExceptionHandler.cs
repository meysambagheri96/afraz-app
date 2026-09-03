using Afraz.Application.Features.Authentication;
using Afraz.Application.Common.Validation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Afraz.Api;

internal sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger,
    IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is AuthenticationException or AuthenticationConflictException)
            logger.LogWarning("Authentication request failed for trace {TraceId}", httpContext.TraceIdentifier);
        else
            logger.LogError(exception, "Unhandled exception for trace {TraceId}", httpContext.TraceIdentifier);

        var statusCode = exception switch
        {
            RequestValidationException => StatusCodes.Status400BadRequest,
            AuthenticationException => StatusCodes.Status401Unauthorized,
            AuthenticationConflictException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError,
        };
        ProblemDetails problemDetails = exception is RequestValidationException validationException
            ? new ValidationProblemDetails(
                validationException.Errors
                    .ToDictionary(
                        pair => pair.Key,
                        pair => pair.Value))
                {
                    Status = statusCode,
                    Title = "Validation failed.",
                }
            : new ProblemDetails
            {
                Status = statusCode,
                Title = exception switch
                {
                    AuthenticationException => "Authentication failed.",
                    AuthenticationConflictException => "Authentication conflict.",
                    _ => "An unexpected error occurred.",
                },
                Detail = exception is AuthenticationException or AuthenticationConflictException
                    ? exception.Message
                    : null,
            };

        problemDetails.Instance = httpContext.Request.Path;
        httpContext.Response.StatusCode = statusCode;

        await problemDetailsService.WriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails = problemDetails,
        });

        return true;
    }
}
