using Afraz.Application.Common.Validation;
using Infra.Commands;

namespace Afraz.Application.Features.Authentication.Login;

public sealed record LoginCommand(string Phone, string Password, string DialingCode = "+98") : ICommand
{
    public override string ToString() => nameof(LoginCommand);
}

internal sealed class LoginValidator : ICommandValidator<LoginCommand>
{
    public ValueTask ValidateAsync(LoginCommand command)
    {
        ValidationFailure.ThrowIf(string.IsNullOrWhiteSpace(command.Phone), nameof(command.Phone), "Phone is required.");
        ValidationFailure.ThrowIf(string.IsNullOrWhiteSpace(command.Password) || command.Password.Length > 128, nameof(command.Password), "Password is required and cannot exceed 128 characters.");
        return ValueTask.CompletedTask;
    }
}

internal sealed class LoginHandler(
    IAuthRepository repository,
    ISecretHasher hasher,
    ITokenService tokenService) : ICommandHandler<LoginCommand, AuthTokensResponse>
{
    public async Task<AuthTokensResponse> HandleAsync(LoginCommand command, CancellationToken cancellationToken)
    {
        var phone = AuthNormalization.Phone(command.Phone);
        var user = await repository.FindByPhoneAsync(command.DialingCode, phone, cancellationToken);
        if (user is null || !user.IsActive || user.PasswordHash is null || !hasher.Verify(command.Password, user.PasswordHash))
            throw new AuthenticationException("Invalid phone number or password.");

        return await AuthHandlerHelpers.CreateSessionAsync(
            user, "Password", phone, tokenService, repository, DateTime.UtcNow, cancellationToken);
    }
}
