using Afraz.Domain.Users;

namespace Afraz.Application.Features.Authentication;

public interface IAuthRepository
{
    Task<User?> FindByPhoneAsync(string dialingCode, string phone, CancellationToken cancellationToken);
    Task<User?> FindByEmailOrGoogleSubjectAsync(string email, string subject, CancellationToken cancellationToken);
    Task<User?> FindByIdAsync(int userId, CancellationToken cancellationToken);
    Task<User?> FindByRefreshTokenHashAsync(string tokenHash, CancellationToken cancellationToken);
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}

public interface ISecretHasher
{
    string Hash(string value);
    bool Verify(string value, string hash);
}

public interface ITokenService
{
    (string Value, DateTime ExpiresAt) CreateAccessToken(User user, DateTime now);
    IssuedToken CreateRefreshToken(DateTime now);
    string HashRefreshToken(string token);
}

public interface IOtpSender
{
    Task SendAsync(string dialingCode, string phone, string code, CancellationToken cancellationToken);
}

public interface IOtpCodeGenerator
{
    string Generate();
}

public interface IGoogleIdentityService
{
    Task<GoogleIdentity> GetIdentityAsync(string authorizationCode, CancellationToken cancellationToken);
}

public interface ICurrentUser
{
    int UserId { get; }
}

public sealed class AuthenticationException(string message) : Exception(message);
public sealed class AuthenticationConflictException(string message) : Exception(message);

internal static class AuthNormalization
{
    public static string Phone(string value)
    {
        var digits = new string(value.Where(char.IsDigit).ToArray());
        if (digits.StartsWith("98", StringComparison.Ordinal) && digits.Length == 12) digits = digits[2..];
        if (digits.StartsWith('0') && digits.Length == 11) digits = digits[1..];
        return digits;
    }
}
