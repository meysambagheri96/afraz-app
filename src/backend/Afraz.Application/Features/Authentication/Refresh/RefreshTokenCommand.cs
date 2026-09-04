using Afraz.Application.Common.Validation;
using Infra.Commands;

namespace Afraz.Application.Features.Authentication.Refresh;

public sealed record RefreshTokenCommand(string RefreshToken) : ICommand
{
    public override string ToString() => nameof(RefreshTokenCommand);
}

internal sealed class RefreshTokenValidator : ICommandValidator<RefreshTokenCommand>
{
    public ValueTask ValidateAsync(RefreshTokenCommand command)
    {
        ValidationFailure.ThrowIf(string.IsNullOrWhiteSpace(command.RefreshToken), nameof(command.RefreshToken), "Refresh token is required.");
        return ValueTask.CompletedTask;
    }
}

internal sealed class RefreshTokenHandler(
    IAuthRepository repository,
    ITokenService tokenService) : ICommandHandler<RefreshTokenCommand, AuthTokensResponse>
{
    public async Task<AuthTokensResponse> HandleAsync(RefreshTokenCommand command, CancellationToken cancellationToken)
    {
        var tokenHash = tokenService.HashRefreshToken(command.RefreshToken);
        var user = await repository.FindByRefreshTokenHashAsync(tokenHash, cancellationToken);
        var session = user?.Sessions.SingleOrDefault(item => item.RefreshTokenHash == tokenHash);
        var now = DateTime.UtcNow;
        if (user is null || session is null || !user.IsActive || !session.IsActive(now))
            throw new AuthenticationException("The refresh token is invalid or expired.");

        var accessToken = tokenService.CreateAccessToken(user, now);
        var replacement = tokenService.CreateRefreshToken(now);
        session.Revoke(now, replacement.Hash);
        user.StartSession(replacement.Hash, replacement.ExpiresAt, now);
        await repository.SaveChangesAsync(cancellationToken);

        return new AuthTokensResponse(
            accessToken.Value,
            accessToken.ExpiresAt,
            replacement.Value,
            replacement.ExpiresAt,
            UserResponseMapper.Map(user));
    }
}
