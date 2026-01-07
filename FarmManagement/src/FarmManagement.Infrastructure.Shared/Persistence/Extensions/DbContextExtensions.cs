using FarmManagement.Infrastructure.Shared.Outbox.Models;
using FarmManagement.Infrastructure.Shared.Persistence.EF;
using FarmManagement.SharedKernel.Domain;
using System.Text.Json;

namespace FarmManagement.Infrastructure.Shared.Persistence.Extensions;

public static class DbContextExtensions
{
    public static async Task SaveChangesWithOutboxAsync(
        this BaseDbContext context,
        CancellationToken cancellationToken = default)
    {
        // Get all domain events from aggregates
        var domainEvents = context.ChangeTracker
            .Entries<AggregateRoot>()
            .SelectMany(e => e.Entity.DomainEvents)
            .ToList();

        // Save changes first
        await context.SaveChangesAsync(cancellationToken);

        // Convert domain events to outbox messages
        foreach (var domainEvent in domainEvents)
        {
            var outboxMessage = new OutboxMessage
            {
                Id = Guid.NewGuid(),
                EventType = domainEvent.GetType().AssemblyQualifiedName ?? domainEvent.GetType().FullName!,
                Payload = JsonSerializer.Serialize(domainEvent, domainEvent.GetType()),
                CreatedAt = DateTime.UtcNow,
                Status = OutboxMessageStatus.Pending,
                RetryCount = 0
            };

            context.Set<OutboxMessage>().Add(outboxMessage);
        }

        // Clear domain events from aggregates
        foreach (var entry in context.ChangeTracker.Entries<AggregateRoot>())
        {
            entry.Entity.ClearDomainEvents();
        }

        // Save outbox messages
        await context.SaveChangesAsync(cancellationToken);
    }
}

