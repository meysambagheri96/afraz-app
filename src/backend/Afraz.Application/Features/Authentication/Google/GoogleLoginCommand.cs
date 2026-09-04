using Afraz.Application.Common.Validation;
using Infra.Commands;

namespace Afraz.Application.Features.Authentication.Google;

public sealed record GoogleLoginCommand(string AuthorizationCode) : ICommand
{
    public override string ToString() => nameof(GoogleLoginCommand);
}

internal sealed class GoogleLoginValidator : ICommandValidator<GoogleLoginCommand>
{
    public ValueTask ValidateAsync(GoogleLoginCommand command)
    {
        ValidationFailure.ThrowIf(string.IsNullOrWhiteSpace(command.AuthorizationCode) || command.AuthorizationCode.Length > 4096, nameof(command.AuthorizationCode), "Authorization code is required and cannot exceed 4096 characters.");
        return ValueTask.CompletedTask;
    }
}

internal sealed class GoogleLoginHandler(
    IAuthRepository repository,
    IGoogleIdentityService google,
    ITokenService tokenService) : ICommandHandler<GoogleLoginCommand, AuthTokensResponse>
{
    public async Task<AuthTokensResponse> HandleAsync(GoogleLoginCommand command, CancellationToken cancellationToken)
    {
        var identity = await google.GetIdentityAsync(command.AuthorizationCode, cancellationToken);
        var user = await repository.FindByEmailOrGoogleSubjectAsync(identity.Email, identity.Subject, cancellationToken);
        var now = DateTime.UtcNow;
        if (user is null)
        {
            user = new Domain.Users.User($"google-{identity.Subject}", "+0", null, now);
            user.LinkGoogle(identity.Subject, identity.Email, identity.FirstName, identity.LastName, identity.Avatar, now);
            await repository.AddAsync(user, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
        }
        else
        {
            user.LinkGoogle(identity.Subject, identity.Email, identity.FirstName, identity.LastName, identity.Avatar, now);
        }

        return await AuthHandlerHelpers.CreateSessionAsync(
            user, "Google", identity.Subject, tokenService, repository, now, cancellationToken);
    }
}
