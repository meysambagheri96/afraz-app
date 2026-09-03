using Afraz.Domain.Users;

namespace Afraz.Application.Features.Authentication;

internal static class AuthHandlerHelpers
{
    public static async Task<AuthTokensResponse> CreateSessionAsync(
        User user,
        string provider,
        string providerKey,
        ITokenService tokenService,
        IAuthRepository repository,
        DateTime now,
        CancellationToken cancellationToken)
    {
        var accessToken = tokenService.CreateAccessToken(user, now);
        var refreshToken = tokenService.CreateRefreshToken(now);
        user.RecordLogin(provider, providerKey, now);
        user.StartSession(refreshToken.Hash, refreshToken.ExpiresAt, now);
        await repository.SaveChangesAsync(cancellationToken);

        return new AuthTokensResponse(
            accessToken.Value,
            accessToken.ExpiresAt,
            refreshToken.Value,
            refreshToken.ExpiresAt,
            UserResponseMapper.Map(user));
    }
}
