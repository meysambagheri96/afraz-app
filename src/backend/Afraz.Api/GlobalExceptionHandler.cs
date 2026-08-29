using System.Net;
using Afraz.Api.Contracts;
using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;

namespace Afraz.Api;

internal sealed class GlobalExceptionHandler(
    ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Unhandled exception for trace {TraceId}", httpContext.TraceIdentifier);

        var isValidationError = exception is ValidationException;
        var statusCode = isValidationError
            ? HttpStatusCode.BadRequest
            : HttpStatusCode.InternalServerError;
        var errors = exception is ValidationException validationException
            ? validationException.Errors
                .GroupBy(failure => failure.PropertyName)
                .Select(group => new ApiErrorEntry(
                    group.Key,
                    (int)HttpStatusCode.BadRequest,
                    group.Select(failure => failure.ErrorMessage).ToArray()))
                .ToArray()
            : [];
        var errorMessage = isValidationError
            ? "Validation failed."
            : "An unexpected error occurred.";

        httpContext.Response.StatusCode = (int)statusCode;

        await httpContext.Response.WriteAsJsonAsync(
            Envelop<object?>.HandledError(statusCode, errors, errorMessage),
            cancellationToken);

        return true;
    }
}
