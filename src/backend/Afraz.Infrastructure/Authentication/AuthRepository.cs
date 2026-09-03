using Afraz.Application.Features.Authentication;
using Afraz.Domain.Users;
using Afraz.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Afraz.Infrastructure.Authentication;

public sealed class AuthRepository(AfrazDbContext dbContext) : IAuthRepository
{
    private IQueryable<User> UsersWithAuthData => dbContext.Users
        .Include(x => x.Roles)
        .Include(x => x.Otps)
        .Include(x => x.Logins)
        .Include(x => x.Sessions);

    public Task<User?> FindByPhoneAsync(string dialingCode, string phone, CancellationToken cancellationToken) =>
        UsersWithAuthData.SingleOrDefaultAsync(x => x.DialingCode == dialingCode && x.Phone == phone, cancellationToken);

    public Task<User?> FindByEmailOrGoogleSubjectAsync(string email, string subject, CancellationToken cancellationToken) =>
        UsersWithAuthData.SingleOrDefaultAsync(x => x.Email == email || x.GoogleSubject == subject, cancellationToken);

    public Task<User?> FindByIdAsync(int userId, CancellationToken cancellationToken) =>
        UsersWithAuthData.SingleOrDefaultAsync(x => x.UserId == userId, cancellationToken);

    public Task<User?> FindByRefreshTokenHashAsync(string tokenHash, CancellationToken cancellationToken) =>
        UsersWithAuthData.SingleOrDefaultAsync(x => x.Sessions.Any(session => session.RefreshTokenHash == tokenHash), cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken) =>
        await dbContext.Users.AddAsync(user, cancellationToken);

    public Task SaveChangesAsync(CancellationToken cancellationToken) => dbContext.SaveChangesAsync(cancellationToken);
}
