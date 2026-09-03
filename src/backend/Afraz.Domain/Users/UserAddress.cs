using Domain;

namespace Afraz.Domain.Users;

public sealed class UserAddress : Entity
{
    private UserAddress() { }

    internal UserAddress(string title, string address, string? postalCode)
    {
        Title = title;
        Address = address;
        PostalCode = postalCode;
    }

    public int UserAddressId { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string? PostalCode { get; private set; }
    public int UserId { get; private set; }
}
