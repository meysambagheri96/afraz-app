using Domain;

namespace Afraz.Domain.Users;

public sealed class User : AggregateRoot
{
    private readonly List<UserRole> _roles = [];
    private readonly List<UserAddress> _addresses = [];
    private readonly List<UserOtp> _otps = [];
    private readonly List<UserLogin> _logins = [];
    private readonly List<UserSession> _sessions = [];

    private User() { }

    public User(string phone, string dialingCode, string? passwordHash, DateTime now)
    {
        if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Phone is required.", nameof(phone));
        if (string.IsNullOrWhiteSpace(dialingCode)) throw new ArgumentException("Dialing code is required.", nameof(dialingCode));

        Id = Guid.NewGuid();
        Phone = phone;
        DialingCode = dialingCode;
        PasswordHash = passwordHash;
        IsActive = passwordHash is not null;
        ModifiedDate = now;
        _roles.Add(new UserRole("Customer"));
    }

    public int UserId { get; private set; }
    public string FirstName { get; private set; } = string.Empty;
    public string LastName { get; private set; } = string.Empty;
    public string? NationalCode { get; private set; }
    public string? ShebaNumber { get; private set; }
    public string? CardNumber { get; private set; }
    public string? AccountNumber { get; private set; }
    public string Phone { get; private set; } = string.Empty;
    public string DialingCode { get; private set; } = string.Empty;
    public string? Email { get; private set; }
    public bool IsActive { get; private set; }
    public string? Avatar { get; private set; }
    public DateTime? LastLoginDate { get; private set; }
    public Gender? Gender { get; private set; }
    public DateTime? BirthDate { get; private set; }
    public DateTime ModifiedDate { get; private set; }
    public int ModifiedBy { get; private set; }
    public int CreatedBy { get; private set; }
    public string? PasswordHash { get; private set; }
    public string? GoogleSubject { get; private set; }
    public IReadOnlyCollection<UserRole> Roles => _roles;
    public IReadOnlyCollection<UserAddress> Addresses => _addresses;
    public IReadOnlyCollection<UserOtp> Otps => _otps;
    public IReadOnlyCollection<UserLogin> Logins => _logins;
    public IReadOnlyCollection<UserSession> Sessions => _sessions;

    public void CompleteRegistration(string firstName, string lastName, string passwordHash, DateTime now)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        PasswordHash = passwordHash;
        IsActive = true;
        ModifiedDate = now;
    }

    public void UpdateProfile(
        string firstName,
        string lastName,
        string? email,
        string? avatar,
        string? nationalCode,
        string? shebaNumber,
        string? cardNumber,
        string? accountNumber,
        Gender? gender,
        DateTime? birthDate,
        DateTime now)
    {
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Email = string.IsNullOrWhiteSpace(email) ? null : email.Trim().ToLowerInvariant();
        Avatar = string.IsNullOrWhiteSpace(avatar) ? null : avatar.Trim();
        NationalCode = string.IsNullOrWhiteSpace(nationalCode) ? null : nationalCode.Trim();
        ShebaNumber = string.IsNullOrWhiteSpace(shebaNumber) ? null : shebaNumber.Trim();
        CardNumber = string.IsNullOrWhiteSpace(cardNumber) ? null : cardNumber.Trim();
        AccountNumber = string.IsNullOrWhiteSpace(accountNumber) ? null : accountNumber.Trim();
        Gender = gender;
        BirthDate = birthDate;
        ModifiedDate = now;
    }

    public void LinkGoogle(string subject, string email, string firstName, string lastName, string? avatar, DateTime now)
    {
        GoogleSubject = subject;
        Email = email.Trim().ToLowerInvariant();
        FirstName = firstName.Trim();
        LastName = lastName.Trim();
        Avatar = avatar;
        IsActive = true;
        ModifiedDate = now;
    }

    public UserOtp IssueOtp(string codeHash, DateTime expiresAt)
    {
        var otp = new UserOtp(codeHash, expiresAt);
        _otps.Add(otp);
        return otp;
    }

    public UserSession StartSession(string tokenHash, DateTime expiresAt, DateTime now)
    {
        var session = new UserSession(tokenHash, expiresAt, now);
        _sessions.Add(session);
        return session;
    }

    public void RecordLogin(string provider, string providerKey, DateTime now)
    {
        LastLoginDate = now;
        _logins.Add(new UserLogin(provider, providerKey, now));
    }

    public void Activate(DateTime now)
    {
        IsActive = true;
        ModifiedDate = now;
    }

    public void Deactivate(DateTime now)
    {
        IsActive = false;
        ModifiedDate = now;
        foreach (var session in _sessions.Where(session => session.IsActive(now))) session.Revoke(now);
    }
}
