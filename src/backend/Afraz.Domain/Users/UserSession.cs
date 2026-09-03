using Domain;

namespace Afraz.Domain.Users;

public sealed class UserSession : Entity
{
    private UserSession() { }

    internal UserSession(string refreshTokenHash, DateTime expiresAt, DateTime createdAt)
    {
        RefreshTokenHash = refreshTokenHash;
        ExpiresAt = expiresAt;
        CreatedAt = createdAt;
    }

    public long UserSessionId { get; private set; }
    public string RefreshTokenHash { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? RevokedAt { get; private set; }
    public string? ReplacedByTokenHash { get; private set; }
    public int UserId { get; private set; }
    public bool IsActive(DateTime now) => RevokedAt is null && ExpiresAt > now;
    public void Revoke(DateTime now, string? replacedByTokenHash = null)
    {
        if (RevokedAt is not null) return;
        RevokedAt = now;
        ReplacedByTokenHash = replacedByTokenHash;
    }
}
