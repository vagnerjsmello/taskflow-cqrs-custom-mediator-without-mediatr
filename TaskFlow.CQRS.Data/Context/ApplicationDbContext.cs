using Microsoft.EntityFrameworkCore;
using TaskFlow.CQRS.Application.Features.Tasks.Models;

namespace TaskFlow.CQRS.Data.Context;

public class ApplicationDbContext : DbContext
{
    public DbSet<TaskModel> Tasks => Set<TaskModel>();

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var clrType = entityType.ClrType;

            if (typeof(Entity).IsAssignableFrom(clrType))
            {
                modelBuilder.Entity(clrType)
                    .Property(nameof(Entity.CreatedAt))
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                modelBuilder.Entity(clrType)
                    .Property(nameof(Entity.UpdatedAt))
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");
            }
        }
    }


    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var now = DateTime.UtcNow;

        foreach (var entry in ChangeTracker.Entries<Entity>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = now;
                entry.Entity.UpdatedAt = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedAt = now;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

}
