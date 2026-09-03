using System.Net.Http.Json;
using Afraz.Application.Features.Authentication;
using Microsoft.Extensions.Options;

namespace Afraz.Infrastructure.Authentication;

public sealed class GoogleIdentityService(HttpClient httpClient, IOptions<GoogleOptions> options)
    : IGoogleIdentityService
{
    private readonly GoogleOptions _options = options.Value;

    public async Task<GoogleIdentity> GetIdentityAsync(string authorizationCode, CancellationToken cancellationToken)
    {
        EnsureConfigured();
        using var tokenResponse = await httpClient.PostAsync(
            "https://oauth2.googleapis.com/token",
            new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["code"] = authorizationCode,
                ["client_id"] = _options.ClientId,
                ["client_secret"] = _options.ClientSecret,
                ["redirect_uri"] = _options.RedirectUri,
                ["grant_type"] = "authorization_code",
            }),
            cancellationToken);
        if (!tokenResponse.IsSuccessStatusCode)
            throw new AuthenticationException("Google authorization code could not be verified.");

        var tokens = await tokenResponse.Content.ReadFromJsonAsync<GoogleTokenResponse>(cancellationToken)
            ?? throw new AuthenticationException("Google returned an invalid token response.");
        using var userRequest = new HttpRequestMessage(HttpMethod.Get, "https://openidconnect.googleapis.com/v1/userinfo");
        userRequest.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokens.AccessToken);
        using var userResponse = await httpClient.SendAsync(userRequest, cancellationToken);
        if (!userResponse.IsSuccessStatusCode)
            throw new AuthenticationException("Google identity could not be read.");

        var profile = await userResponse.Content.ReadFromJsonAsync<GoogleUserInfo>(cancellationToken)
            ?? throw new AuthenticationException("Google returned an invalid identity response.");
        if (!profile.EmailVerified || string.IsNullOrWhiteSpace(profile.Email) || string.IsNullOrWhiteSpace(profile.Subject))
            throw new AuthenticationException("A verified Google email is required.");

        return new GoogleIdentity(
            profile.Subject,
            profile.Email,
            profile.GivenName ?? string.Empty,
            profile.FamilyName ?? string.Empty,
            profile.Picture);
    }

    private void EnsureConfigured()
    {
        if (string.IsNullOrWhiteSpace(_options.ClientId) || string.IsNullOrWhiteSpace(_options.ClientSecret))
            throw new InvalidOperationException("Google OAuth server configuration is missing.");
    }

    private sealed record GoogleTokenResponse(
        [property: System.Text.Json.Serialization.JsonPropertyName("access_token")] string AccessToken);

    private sealed record GoogleUserInfo(
        [property: System.Text.Json.Serialization.JsonPropertyName("sub")] string Subject,
        [property: System.Text.Json.Serialization.JsonPropertyName("email")] string Email,
        [property: System.Text.Json.Serialization.JsonPropertyName("email_verified")] bool EmailVerified,
        [property: System.Text.Json.Serialization.JsonPropertyName("given_name")] string? GivenName,
        [property: System.Text.Json.Serialization.JsonPropertyName("family_name")] string? FamilyName,
        [property: System.Text.Json.Serialization.JsonPropertyName("picture")] string? Picture);
}
