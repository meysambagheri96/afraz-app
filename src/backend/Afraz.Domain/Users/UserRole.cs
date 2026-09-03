using Domain;

namespace Afraz.Domain.Users;

public sealed class UserRole : Entity
{
    private UserRole() { }

    internal UserRole(string name)
    {
        Name = name;
    }

    public int UserRoleId { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public int UserId { get; private set; }
}
