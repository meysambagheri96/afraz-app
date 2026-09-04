using System.Text.RegularExpressions;
using Afraz.Application.Common.Validation;
using Infra.Commands;

namespace Afraz.Application.Features.Authentication.Otp;

public sealed record VerifyOtpCommand(string Phone, string Code, string DialingCode = "+98") : ICommand
{
    public override string ToString() => nameof(VerifyOtpCommand);
}

internal sealed class VerifyOtpValidator : ICommandValidator<VerifyOtpCommand>
{
    public ValueTask ValidateAsync(VerifyOtpCommand command)
    {
        ValidationFailure.ThrowIf(string.IsNullOrWhiteSpace(command.Phone), nameof(command.Phone), "Phone is required.");
        ValidationFailure.ThrowIf(!Regex.IsMatch(command.Code ?? string.Empty, "^\\d{5}$"), nameof(command.Code), "Verification code must contain five digits.");
        return ValueTask.CompletedTask;
    }
}

internal sealed class VerifyOtpHandler(
    IAuthRepository repository,
    ISecretHasher hasher,
    ITokenService tokenService) : ICommandHandler<VerifyOtpCommand, AuthTokensResponse>
{
    public async Task<AuthTokensResponse> HandleAsync(VerifyOtpCommand command, CancellationToken cancellationToken)
    {
        var phone = AuthNormalization.Phone(command.Phone);
        var user = await repository.FindByPhoneAsync(command.DialingCode, phone, cancellationToken);
        var otp = user?.Otps.OrderByDescending(item => item.ExpiresAt).FirstOrDefault();
        var now = DateTime.UtcNow;
        if (user is null || otp is null || !otp.CanVerify(now))
            throw new AuthenticationException("The verification code is invalid or expired.");

        if (!hasher.Verify(command.Code, otp.CodeHash))
        {
            otp.MarkFailed();
            await repository.SaveChangesAsync(cancellationToken);
            throw new AuthenticationException("The verification code is invalid or expired.");
        }

        otp.MarkUsed(now);
        user.Activate(now);
        return await AuthHandlerHelpers.CreateSessionAsync(
            user, "Otp", phone, tokenService, repository, now, cancellationToken);
    }
}
