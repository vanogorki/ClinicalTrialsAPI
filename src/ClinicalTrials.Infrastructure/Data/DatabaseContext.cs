using ClinicalTrials.Domain.Entities;
using ClinicalTrials.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace ClinicalTrials.Infrastructure.Data;

public sealed class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<ClinicalTrial> ClinicalTrials { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClinicalTrial>()
            .Property(e => e.Participants)
            .HasDefaultValue(1);
        modelBuilder.Entity<ClinicalTrial>()
            .HasIndex(e => e.TrialId)
            .IsUnique();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var item in ChangeTracker.Entries<BaseEntity>().AsEnumerable())
            switch (item.State)
            {
                case EntityState.Added:
                    item.Entity.CreatedAt = DateTime.UtcNow;
                    item.Entity.EntityStatus = EntityStatus.Active;
                    break;
                case EntityState.Modified:
                    item.Entity.LastModifiedAt = DateTime.UtcNow;
                    break;
                case EntityState.Detached:
                case EntityState.Unchanged:
                case EntityState.Deleted:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

        return base.SaveChangesAsync(cancellationToken);
    }
}