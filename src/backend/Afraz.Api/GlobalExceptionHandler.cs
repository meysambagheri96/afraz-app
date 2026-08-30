using FluentValidation;
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
        logger.LogError(exception, "Unhandled exception for trace {TraceId}", httpContext.TraceIdentifier);

        var isValidationError = exception is ValidationException;
        var statusCode = isValidationError
            ? StatusCodes.Status400BadRequest
            : StatusCodes.Status500InternalServerError;
        ProblemDetails problemDetails = exception is ValidationException validationException
            ? new ValidationProblemDetails(
                validationException.Errors
                    .GroupBy(failure => failure.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(failure => failure.ErrorMessage).ToArray()))
                {
                    Status = statusCode,
                    Title = "Validation failed.",
                }
            : new ProblemDetails
            {
                Status = statusCode,
                Title = "An unexpected error occurred.",
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
