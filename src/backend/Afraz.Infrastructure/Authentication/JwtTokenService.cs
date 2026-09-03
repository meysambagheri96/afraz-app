using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Afraz.Application.Features.Authentication;
using Afraz.Domain.Users;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Afraz.Infrastructure.Authentication;

public sealed class JwtTokenService(IOptions<JwtOptions> options) : ITokenService
{
    private readonly JwtOptions _options = options.Value;

    public (string Value, DateTime ExpiresAt) CreateAccessToken(User user, DateTime now)
    {
        var expiresAt = now.AddMinutes(_options.AccessTokenMinutes);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.UserId.ToString(System.Globalization.CultureInfo.InvariantCulture)),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
            new(ClaimTypes.MobilePhone, $"{user.DialingCode}{user.Phone}"),
        };
        claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey)),
            SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            _options.Issuer,
            _options.Audience,
            claims,
            now,
            expiresAt,
            credentials);
        return (new JwtSecurityTokenHandler().WriteToken(token), expiresAt);
    }

    public IssuedToken CreateRefreshToken(DateTime now)
    {
        var value = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        return new IssuedToken(value, HashRefreshToken(value), now.AddDays(_options.RefreshTokenDays));
    }

    public string HashRefreshToken(string token) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
}
