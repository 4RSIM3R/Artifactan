using Artifactan.Entities.Base;
using Artifactan.Entities.Master;
using Microsoft.EntityFrameworkCore;

namespace Artifactan.Entities;

public class ApplicationDbContext : DbContext
{

    private static long instanceCount;

    public ApplicationDbContext(DbContextOptions options)
        : base(options)
        => Interlocked.Increment(ref instanceCount);

    public DbSet<User> Users { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        AddTimestamp();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        
        builder.Entity<User>()
            .HasIndex(p => p.Email)
            .IsUnique(true);

        
    }

    public void AddTimestamp()
    {

        // get current entities
        var entities = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

        foreach (var entity in entities)
        {
            var now = DateTime.UtcNow;
            if (entity.State == EntityState.Added)
            {
                ((BaseEntity)entity.Entity).CreatedAt = now;
            }
            else
            {
                ((BaseEntity)entity.Entity).UpdatedAt = now;
            }
        }

    }
}