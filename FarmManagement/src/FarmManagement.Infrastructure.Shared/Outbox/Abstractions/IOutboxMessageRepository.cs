namespace FarmManagement.Infrastructure.Shared.Outbox.Abstractions;

public interface IOutboxMessageRepository
{
    Task AddAsync(Models.OutboxMessage message, CancellationToken cancellationToken = default);
    Task<IEnumerable<Models.OutboxMessage>> GetPendingMessagesAsync(int batchSize, CancellationToken cancellationToken = default);
    Task MarkAsProcessingAsync(Guid id, CancellationToken cancellationToken = default);
    Task MarkAsProcessedAsync(Guid id, CancellationToken cancellationToken = default);
    Task MarkAsFailedAsync(Guid id, string error, CancellationToken cancellationToken = default);
    Task IncrementRetryCountAsync(Guid id, CancellationToken cancellationToken = default);
    Task ResetToPendingAsync(Guid id, CancellationToken cancellationToken = default);
}