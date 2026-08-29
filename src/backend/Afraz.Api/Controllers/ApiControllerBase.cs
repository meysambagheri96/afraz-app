using System.Net;
using Afraz.Api.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Afraz.Api.Controllers;

public abstract class ApiControllerBase : ControllerBase
{
    protected ActionResult<Envelop<T>> ApiOk<T>(
        T data,
        PaginationInfo? pagination = null)
    {
        return Ok(Envelop<T>.Success(HttpStatusCode.OK, data, pagination));
    }

    protected ObjectResult ApiError<T>(
        HttpStatusCode statusCode,
        string errorMessage,
        IReadOnlyCollection<ApiErrorEntry>? errors = null)
    {
        return StatusCode(
            (int)statusCode,
            Envelop<T>.HandledError(statusCode, errors, errorMessage));
    }
}
