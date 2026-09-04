using System.Text.RegularExpressions;
using Afraz.Application.Common.Validation;
using Infra.Commands;

namespace Afraz.Application.Features.Authentication.Otp;

public sealed record RequestOtpCommand(string Phone, string DialingCode = "+98") : ICommand
{
    public override string ToString() => nameof(RequestOtpCommand);
}
public sealed record RequestOtpResponse(DateTime ExpiresAt);

internal sealed class RequestOtpValidator : ICommandValidator<RequestOtpCommand>
{
    public ValueTask ValidateAsync(RequestOtpCommand command)
    {
        ValidationFailure.ThrowIf(!Regex.IsMatch(command.Phone ?? string.Empty, "^(?:09\\d{9}|9\\d{9}|\\+?989\\d{9})$"), nameof(command.Phone), "Phone number is invalid.");
        return ValueTask.CompletedTask;
    }
}

internal sealed class RequestOtpHandler(
    IAuthRepository repository,
    ISecretHasher hasher,
    IOtpCodeGenerator codeGenerator,
    IOtpSender sender) : ICommandHandler<RequestOtpCommand, RequestOtpResponse>
{
    public async Task<RequestOtpResponse> HandleAsync(RequestOtpCommand command, CancellationToken cancellationToken)
    {
        var phone = AuthNormalization.Phone(command.Phone);
        var now = DateTime.UtcNow;
        var user = await repository.FindByPhoneAsync(command.DialingCode, phone, cancellationToken);
        if (user is null)
        {
            user = new Domain.Users.User(phone, command.DialingCode, null, now);
            await repository.AddAsync(user, cancellationToken);
        }

        var code = codeGenerator.Generate();
        var expiresAt = now.AddMinutes(2);
        user.IssueOtp(hasher.Hash(code), expiresAt);
        await repository.SaveChangesAsync(cancellationToken);
        await sender.SendAsync(command.DialingCode, phone, code, cancellationToken);
        return new RequestOtpResponse(expiresAt);
    }
}
