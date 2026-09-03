using Afraz.Application.Features.Authentication;
using Afraz.Application.Common.Validation;
using Afraz.Domain.Users;
using System.Text.RegularExpressions;
using Infra.Commands;

namespace Afraz.Application.Features.Users.UpdateCurrentUser;

public sealed record UpdateCurrentUserCommand(
    string FirstName,
    string LastName,
    string? Email,
    string? Avatar,
    string? NationalCode,
    string? ShebaNumber,
    string? CardNumber,
    string? AccountNumber,
    Gender? Gender,
    DateTime? BirthDate) : ICommand;

internal sealed class UpdateCurrentUserValidator : ICommandValidator<UpdateCurrentUserCommand>
{
    public ValueTask ValidateAsync(UpdateCurrentUserCommand command)
    {
        ValidationFailure.ThrowIf(string.IsNullOrWhiteSpace(command.FirstName) || command.FirstName.Length > 100, nameof(command.FirstName), "First name is required and cannot exceed 100 characters.");
        ValidationFailure.ThrowIf(string.IsNullOrWhiteSpace(command.LastName) || command.LastName.Length > 100, nameof(command.LastName), "Last name is required and cannot exceed 100 characters.");
        ValidationFailure.ThrowIf(!string.IsNullOrWhiteSpace(command.Email) && (command.Email.Length > 256 || !command.Email.Contains("@", StringComparison.Ordinal)), nameof(command.Email), "Email is invalid.");
        ValidationFailure.ThrowIf(command.Avatar?.Length > 2048, nameof(command.Avatar), "Avatar URL cannot exceed 2048 characters.");
        ValidationFailure.ThrowIf(!MatchesOptional(command.NationalCode, "^\\d{10}$"), nameof(command.NationalCode), "National code must contain 10 digits.");
        ValidationFailure.ThrowIf(!MatchesOptional(command.ShebaNumber, "^(?:IR)?\\d{24}$"), nameof(command.ShebaNumber), "Sheba number is invalid.");
        ValidationFailure.ThrowIf(!MatchesOptional(command.CardNumber, "^\\d{16}$"), nameof(command.CardNumber), "Card number must contain 16 digits.");
        ValidationFailure.ThrowIf(command.AccountNumber?.Length > 32, nameof(command.AccountNumber), "Account number cannot exceed 32 characters.");
        ValidationFailure.ThrowIf(command.BirthDate >= DateTime.UtcNow, nameof(command.BirthDate), "Birth date must be in the past.");
        return ValueTask.CompletedTask;
    }

    private static bool MatchesOptional(string? value, string pattern) =>
        string.IsNullOrWhiteSpace(value) || Regex.IsMatch(value, pattern);
}

internal sealed class UpdateCurrentUserHandler(IAuthRepository repository, ICurrentUser currentUser)
    : ICommandHandler<UpdateCurrentUserCommand, UserResponse>
{
    public async Task<UserResponse> HandleAsync(UpdateCurrentUserCommand command, CancellationToken cancellationToken)
    {
        var user = await repository.FindByIdAsync(currentUser.UserId, cancellationToken);
        if (user is null || !user.IsActive) throw new AuthenticationException("User was not found.");
        user.UpdateProfile(
            command.FirstName,
            command.LastName,
            command.Email,
            command.Avatar,
            command.NationalCode,
            command.ShebaNumber,
            command.CardNumber,
            command.AccountNumber,
            command.Gender,
            command.BirthDate,
            DateTime.UtcNow);
        await repository.SaveChangesAsync(cancellationToken);
        return UserResponseMapper.Map(user);
    }
}
