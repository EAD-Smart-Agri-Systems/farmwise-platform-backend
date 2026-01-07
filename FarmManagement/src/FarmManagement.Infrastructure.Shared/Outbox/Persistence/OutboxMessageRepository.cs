using FarmManagement.Infrastructure.Shared.Outbox.Abstractions;
using FarmManagement.Infrastructure.Shared.Persistence.Abstractions;
using FarmManagement.Infrastructure.Shared.Persistence.EF;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Infrastructure.Shared.Outbox.Persistence;

public class OutboxMessageRepository : IOutboxMessageRepository
{
    private readonly BaseDbContext _context;

    public OutboxMessageRepository(BaseDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Models.OutboxMessage message, CancellationToken cancellationToken = default)
    {
        await _context.Set<Models.OutboxMessage>().AddAsync(message, cancellationToken);
    }

    public async Task<IEnumerable<Models.OutboxMessage>> GetPendingMessagesAsync(
        int batchSize,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.OutboxMessage>()
            .Where(m => m.Status == Models.OutboxMessageStatus.Pending)
            .OrderBy(m => m.CreatedAt)
            .Take(batchSize)
            .ToListAsync(cancellationToken);
    }

    public async Task MarkAsProcessingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var message = await _context.Set<Models.OutboxMessage>()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (message != null)
        {
            message.Status = Models.OutboxMessageStatus.Processing;
        }
    }

    public async Task MarkAsProcessedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var message = await _context.Set<Models.OutboxMessage>()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (message != null)
        {
            message.Status = Models.OutboxMessageStatus.Processed;
            message.ProcessedAt = DateTime.UtcNow;
        }
    }

    public async Task MarkAsFailedAsync(Guid id, string error, CancellationToken cancellationToken = default)
    {
        var message = await _context.Set<Models.OutboxMessage>()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (message != null)
        {
            message.Status = Models.OutboxMessageStatus.Failed;
            message.Error = error;
        }
    }

    public async Task IncrementRetryCountAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var message = await _context.Set<Models.OutboxMessage>()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (message != null)
        {
            message.RetryCount++;
        }

        await Task.CompletedTask;
    }

    public async Task ResetToPendingAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var message = await _context.Set<Models.OutboxMessage>()
            .FirstOrDefaultAsync(m => m.Id == id, cancellationToken);

        if (message != null)
        {
            message.Status = Models.OutboxMessageStatus.Pending;
        }
    }
}

