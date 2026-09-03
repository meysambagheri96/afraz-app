using Domain;

namespace Afraz.Domain.Users;

public sealed class UserLogin : Entity
{
    private UserLogin() { }

    internal UserLogin(string provider, string providerKey, DateTime loggedInAt)
    {
        Provider = provider;
        ProviderKey = providerKey;
        LoggedInAt = loggedInAt;
    }

    public long UserLoginId { get; private set; }
    public string Provider { get; private set; } = string.Empty;
    public string ProviderKey { get; private set; } = string.Empty;
    public DateTime LoggedInAt { get; private set; }
    public int UserId { get; private set; }
}
