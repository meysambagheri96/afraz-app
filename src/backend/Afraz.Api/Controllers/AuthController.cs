using Afraz.Api.Contracts;
using Afraz.Application.Features.Authentication;
using Afraz.Application.Features.Authentication.Google;
using Afraz.Application.Features.Authentication.Login;
using Afraz.Application.Features.Authentication.Logout;
using Afraz.Application.Features.Authentication.Otp;
using Afraz.Application.Features.Authentication.Refresh;
using Afraz.Application.Features.Authentication.Register;
using Infra.Commands;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Afraz.Api.Controllers;

[ApiController]
[Route("api/auth")]
[EnableRateLimiting("auth")]
public sealed class AuthController(ICommandProcessor commandProcessor) : ApiControllerBase
{
    [HttpPost("register")]
    public async Task<ActionResult<Envelop<AuthTokensResponse>>> RegisterAsync(
        RegisterCommand command,
        CancellationToken cancellationToken) =>
        ApiOk(await commandProcessor.ExecuteAsync<RegisterCommand, AuthTokensResponse>(command, cancellationToken));

    [HttpPost("login")]
    public async Task<ActionResult<Envelop<AuthTokensResponse>>> LoginAsync(
        LoginCommand command,
        CancellationToken cancellationToken) =>
        ApiOk(await commandProcessor.ExecuteAsync<LoginCommand, AuthTokensResponse>(command, cancellationToken));

    [HttpPost("otp/request")]
    public async Task<ActionResult<Envelop<RequestOtpResponse>>> RequestOtpAsync(
        RequestOtpCommand command,
        CancellationToken cancellationToken) =>
        ApiOk(await commandProcessor.ExecuteAsync<RequestOtpCommand, RequestOtpResponse>(command, cancellationToken));

    [HttpPost("otp/verify")]
    public async Task<ActionResult<Envelop<AuthTokensResponse>>> VerifyOtpAsync(
        VerifyOtpCommand command,
        CancellationToken cancellationToken) =>
        ApiOk(await commandProcessor.ExecuteAsync<VerifyOtpCommand, AuthTokensResponse>(command, cancellationToken));

    [HttpPost("refresh")]
    public async Task<ActionResult<Envelop<AuthTokensResponse>>> RefreshAsync(
        RefreshTokenCommand command,
        CancellationToken cancellationToken) =>
        ApiOk(await commandProcessor.ExecuteAsync<RefreshTokenCommand, AuthTokensResponse>(command, cancellationToken));

    [HttpPost("logout")]
    public async Task<ActionResult<Envelop<LogoutResponse>>> LogoutAsync(
        LogoutCommand command,
        CancellationToken cancellationToken) =>
        ApiOk(await commandProcessor.ExecuteAsync<LogoutCommand, LogoutResponse>(command, cancellationToken));

    [HttpPost("google")]
    public async Task<ActionResult<Envelop<AuthTokensResponse>>> GoogleAsync(
        GoogleLoginCommand command,
        CancellationToken cancellationToken) =>
        ApiOk(await commandProcessor.ExecuteAsync<GoogleLoginCommand, AuthTokensResponse>(command, cancellationToken));
}
