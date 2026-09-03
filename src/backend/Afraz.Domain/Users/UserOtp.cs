using Domain;

namespace Afraz.Domain.Users;

public sealed class UserOtp : Entity
{
    private UserOtp() { }

    internal UserOtp(string codeHash, DateTime expiresAt)
    {
        CodeHash = codeHash;
        ExpiresAt = expiresAt;
    }

    public long UserOtpId { get; private set; }
    public string CodeHash { get; private set; } = string.Empty;
    public DateTime ExpiresAt { get; private set; }
    public DateTime? UsedAt { get; private set; }
    public int FailedAttempts { get; private set; }
    public int UserId { get; private set; }
    public bool CanVerify(DateTime now) => UsedAt is null && FailedAttempts < 5 && ExpiresAt > now;
    public void MarkFailed() => FailedAttempts++;
    public void MarkUsed(DateTime now) => UsedAt = now;
}
