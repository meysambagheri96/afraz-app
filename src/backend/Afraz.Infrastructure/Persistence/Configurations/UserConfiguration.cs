using Afraz.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Afraz.Infrastructure.Persistence.Configurations;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", "auth");
        builder.HasKey(x => x.UserId);
        builder.Property(x => x.UserId).UseIdentityColumn(1000);
        builder.Property(x => x.Id).IsRequired();
        builder.HasIndex(x => x.Id).IsUnique();
        builder.Property(x => x.Phone).HasMaxLength(32).IsRequired();
        builder.Property(x => x.DialingCode).HasMaxLength(8).IsRequired();
        builder.HasIndex(x => new { x.DialingCode, x.Phone }).IsUnique();
        builder.Property(x => x.FirstName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Email).HasMaxLength(256);
        builder.HasIndex(x => x.Email).IsUnique().HasFilter("[Email] IS NOT NULL");
        builder.Property(x => x.GoogleSubject).HasMaxLength(128);
        builder.HasIndex(x => x.GoogleSubject).IsUnique().HasFilter("[GoogleSubject] IS NOT NULL");
        builder.Property(x => x.PasswordHash).HasMaxLength(512);
        builder.Property(x => x.NationalCode).HasMaxLength(16);
        builder.Property(x => x.ShebaNumber).HasMaxLength(32);
        builder.Property(x => x.CardNumber).HasMaxLength(32);
        builder.Property(x => x.AccountNumber).HasMaxLength(32);
        builder.Property(x => x.Avatar).HasMaxLength(2048);
        builder.Property(x => x.ModifiedDate).HasPrecision(0);
        builder.Property(x => x.LastLoginDate).HasPrecision(0);
        builder.Property(x => x.BirthDate).HasColumnType("date");
        builder.Property(x => x.IsActive).HasDefaultValue(false);
        builder.Ignore(x => x.UncommittedChanges);
        builder.Ignore(x => x.Version);
        builder.Ignore(x => x.IsNew);
        builder.Ignore(x => x.StreamName);

        builder.HasMany(x => x.Roles).WithOne().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Addresses).WithOne().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Otps).WithOne().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Logins).WithOne().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(x => x.Sessions).WithOne().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Cascade);
    }
}

public sealed class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
{
    public void Configure(EntityTypeBuilder<UserRole> builder)
    {
        builder.ToTable("UserRoles", "auth");
        builder.HasKey(x => x.UserRoleId);
        builder.Property(x => x.Name).HasMaxLength(64).IsRequired();
        builder.HasIndex(x => new { x.UserId, x.Name }).IsUnique();
    }
}

public sealed class UserAddressConfiguration : IEntityTypeConfiguration<UserAddress>
{
    public void Configure(EntityTypeBuilder<UserAddress> builder)
    {
        builder.ToTable("UserAddresses", "auth");
        builder.HasKey(x => x.UserAddressId);
        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Address).HasMaxLength(1000).IsRequired();
        builder.Property(x => x.PostalCode).HasMaxLength(20);
    }
}

public sealed class UserOtpConfiguration : IEntityTypeConfiguration<UserOtp>
{
    public void Configure(EntityTypeBuilder<UserOtp> builder)
    {
        builder.ToTable("UserOtps", "auth");
        builder.HasKey(x => x.UserOtpId);
        builder.Property(x => x.CodeHash).HasMaxLength(512).IsRequired();
        builder.HasIndex(x => new { x.UserId, x.ExpiresAt });
    }
}

public sealed class UserLoginConfiguration : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.ToTable("UserLogins", "auth");
        builder.HasKey(x => x.UserLoginId);
        builder.Property(x => x.Provider).HasMaxLength(32).IsRequired();
        builder.Property(x => x.ProviderKey).HasMaxLength(256).IsRequired();
        builder.HasIndex(x => new { x.UserId, x.LoggedInAt });
    }
}

public sealed class UserSessionConfiguration : IEntityTypeConfiguration<UserSession>
{
    public void Configure(EntityTypeBuilder<UserSession> builder)
    {
        builder.ToTable("UserSessions", "auth");
        builder.HasKey(x => x.UserSessionId);
        builder.Property(x => x.RefreshTokenHash).HasMaxLength(64).IsRequired();
        builder.Property(x => x.ReplacedByTokenHash).HasMaxLength(64);
        builder.HasIndex(x => x.RefreshTokenHash).IsUnique();
        builder.HasIndex(x => new { x.UserId, x.ExpiresAt });
    }
}
