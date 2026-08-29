using Afraz.Api.Contracts;
using Afraz.Application.Features.Foundation.GetStatus;
using Infra.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Afraz.Api.Controllers;

[ApiController]
[Route("api/status")]
public sealed class StatusController(IQueryProcessor queryProcessor) : ApiControllerBase
{
    [HttpGet]
    [ProducesResponseType<Envelop<GetStatusResponse>>(StatusCodes.Status200OK)]
    public async Task<ActionResult<Envelop<GetStatusResponse>>> GetAsync(
        CancellationToken cancellationToken)
    {
        var response = await queryProcessor.ExecuteAsync(
            new GetStatusQuery(),
            cancellationToken);

        return ApiOk(response);
    }
}
