using System.Text.RegularExpressions;
using Afraz.Application.Common.Validation;
using Infra.Commands;

namespace Afraz.Application.Features.Authentication.Register;

public sealed record RegisterCommand(
    string Phone,
    string Password,
    string FirstName,
    string LastName,
    string DialingCode = "+98") : ICommand
{
    public override string ToString() => nameof(RegisterCommand);
}

internal sealed class RegisterValidator : ICommandValidator<RegisterCommand>
{
    public ValueTask ValidateAsync(RegisterCommand command)
    {
        ValidationFailure.ThrowIf(!Regex.IsMatch(command.Phone ?? string.Empty, "^(?:09\\d{9}|9\\d{9}|\\+?989\\d{9})$"), nameof(command.Phone), "Phone number is invalid.");
        ValidationFailure.ThrowIf(command.Password is null || command.Password.Length is < 8 or > 128, nameof(command.Password), "Password must contain between 8 and 128 characters.");
        ValidationFailure.ThrowIf(string.IsNullOrWhiteSpace(command.FirstName) || command.FirstName.Length > 100, nameof(command.FirstName), "First name is required and cannot exceed 100 characters.");
        ValidationFailure.ThrowIf(string.IsNullOrWhiteSpace(command.LastName) || command.LastName.Length > 100, nameof(command.LastName), "Last name is required and cannot exceed 100 characters.");
        ValidationFailure.ThrowIf(!Regex.IsMatch(command.DialingCode ?? string.Empty, "^\\+\\d{1,4}$"), nameof(command.DialingCode), "Dialing code is invalid.");
        return ValueTask.CompletedTask;
    }
}

internal sealed class RegisterHandler(
    IAuthRepository repository,
    ISecretHasher hasher,
    ITokenService tokenService) : ICommandHandler<RegisterCommand, AuthTokensResponse>
{
    public async Task<AuthTokensResponse> HandleAsync(RegisterCommand command, CancellationToken cancellationToken)
    {
        var phone = AuthNormalization.Phone(command.Phone);
        var now = DateTime.UtcNow;
        var user = await repository.FindByPhoneAsync(command.DialingCode, phone, cancellationToken);
        if (user is not null && (user.IsActive || user.PasswordHash is not null))
            throw new AuthenticationConflictException("A user with this phone number already exists.");
        if (user is null)
        {
            user = new Domain.Users.User(phone, command.DialingCode, null, now);
            await repository.AddAsync(user, cancellationToken);
        }

        user.CompleteRegistration(command.FirstName, command.LastName, hasher.Hash(command.Password), now);
        await repository.SaveChangesAsync(cancellationToken);

        return await AuthHandlerHelpers.CreateSessionAsync(
            user, "Password", phone, tokenService, repository, now, cancellationToken);
    }
}
