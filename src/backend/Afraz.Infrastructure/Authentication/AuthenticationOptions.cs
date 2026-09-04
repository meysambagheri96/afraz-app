namespace Afraz.Infrastructure.Authentication;

public sealed class JwtOptions
{
    public const string SectionName = "Jwt";
    public string Issuer { get; init; } = "Afraz.Api";
    public string Audience { get; init; } = "Afraz.App";
    public string SigningKey { get; init; } = string.Empty;
    public int AccessTokenDays { get; init; } = 30;
    public int RefreshTokenDays { get; init; } = 30;
}

public sealed class GoogleOptions
{
    public const string SectionName = "Google";
    public string ClientId { get; init; } = string.Empty;
    public string ClientSecret { get; init; } = string.Empty;
    public string RedirectUri { get; init; } = "https://afrazstudioqom.ir/signin-google";
}
