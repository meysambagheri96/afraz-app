using Afraz.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Afraz.Infrastructure.Persistence;

public sealed class AfrazDbContext(DbContextOptions<AfrazDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AfrazDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
