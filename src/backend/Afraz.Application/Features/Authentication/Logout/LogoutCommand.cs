using Afraz.Application.Common.Validation;
using Infra.Commands;

namespace Afraz.Application.Features.Authentication.Logout;

public sealed record LogoutCommand(string RefreshToken) : ICommand;
public sealed record LogoutResponse(bool Revoked);

internal sealed class LogoutValidator : ICommandValidator<LogoutCommand>
{
    public ValueTask ValidateAsync(LogoutCommand command)
    {
        ValidationFailure.ThrowIf(string.IsNullOrWhiteSpace(command.RefreshToken), nameof(command.RefreshToken), "Refresh token is required.");
        return ValueTask.CompletedTask;
    }
}

internal sealed class LogoutHandler(IAuthRepository repository, ITokenService tokenService)
    : ICommandHandler<LogoutCommand, LogoutResponse>
{
    public async Task<LogoutResponse> HandleAsync(LogoutCommand command, CancellationToken cancellationToken)
    {
        var hash = tokenService.HashRefreshToken(command.RefreshToken);
        var user = await repository.FindByRefreshTokenHashAsync(hash, cancellationToken);
        var session = user?.Sessions.SingleOrDefault(item => item.RefreshTokenHash == hash);
        if (session is null) return new LogoutResponse(false);
        session.Revoke(DateTime.UtcNow);
        await repository.SaveChangesAsync(cancellationToken);
        return new LogoutResponse(true);
    }
}
