using Afraz.Domain.Users;

namespace Afraz.Application.Features.Authentication;

public sealed record AuthTokensResponse(
    string AccessToken,
    DateTime AccessTokenExpiresAt,
    string RefreshToken,
    DateTime RefreshTokenExpiresAt,
    UserResponse User);

public sealed record UserResponse(
    int UserId,
    string FirstName,
    string LastName,
    string Phone,
    string DialingCode,
    string? Email,
    string? Avatar,
    string? NationalCode,
    string? ShebaNumber,
    string? CardNumber,
    string? AccountNumber,
    Gender? Gender,
    DateTime? BirthDate,
    bool IsActive);

public sealed record IssuedToken(string Value, string Hash, DateTime ExpiresAt);

public sealed record GoogleIdentity(string Subject, string Email, string FirstName, string LastName, string? Avatar);

public static class UserResponseMapper
{
    public static UserResponse Map(User user) => new(
        user.UserId,
        user.FirstName,
        user.LastName,
        user.Phone,
        user.DialingCode,
        user.Email,
        user.Avatar,
        user.NationalCode,
        user.ShebaNumber,
        user.CardNumber,
        user.AccountNumber,
        user.Gender,
        user.BirthDate,
        user.IsActive);
}
