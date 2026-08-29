using Microsoft.EntityFrameworkCore;

namespace Afraz.Infrastructure.Persistence;

public sealed class AfrazDbContext(DbContextOptions<AfrazDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AfrazDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}

