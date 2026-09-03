using Afraz.Api.Contracts;
using Afraz.Application.Features.Authentication;
using Afraz.Application.Features.Users.DeleteCurrentUser;
using Afraz.Application.Features.Users.GetCurrentUser;
using Afraz.Application.Features.Users.UpdateCurrentUser;
using Infra.Commands;
using Infra.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Afraz.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/users/me")]
public sealed class UsersController(ICommandProcessor commandProcessor, IQueryProcessor queryProcessor)
    : ApiControllerBase
{
    [HttpGet]
    public async Task<ActionResult<Envelop<UserResponse>>> GetAsync(CancellationToken cancellationToken) =>
        ApiOk(await queryProcessor.ExecuteAsync(new GetCurrentUserQuery(), cancellationToken));

    [HttpPut]
    public async Task<ActionResult<Envelop<UserResponse>>> UpdateAsync(
        UpdateCurrentUserCommand command,
        CancellationToken cancellationToken) =>
        ApiOk(await commandProcessor.ExecuteAsync<UpdateCurrentUserCommand, UserResponse>(command, cancellationToken));

    [HttpDelete]
    public async Task<ActionResult<Envelop<DeleteCurrentUserResponse>>> DeleteAsync(CancellationToken cancellationToken) =>
        ApiOk(await commandProcessor.ExecuteAsync<DeleteCurrentUserCommand, DeleteCurrentUserResponse>(
            new DeleteCurrentUserCommand(), cancellationToken));
}
