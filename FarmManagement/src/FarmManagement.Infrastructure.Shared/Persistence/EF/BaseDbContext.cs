using FarmManagement.Infrastructure.Shared.Outbox.Models;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Shared.Persistence.EF;

public abstract class BaseDbContext : DbContext
{
    protected BaseDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure OutboxMessage entity
        modelBuilder.Entity<OutboxMessage>(entity =>
        {
            entity.ToTable("OutboxMessages");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.EventType).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Payload).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.Status).IsRequired();
            entity.Property(e => e.RetryCount).IsRequired();
            entity.HasIndex(e => new { e.Status, e.CreatedAt });
        });
    }
}

