using FarmManagement.Infrastructure.Shared.Configuration;
using FarmManagement.Infrastructure.Shared.Messaging.Abstractions;
using FarmManagement.Infrastructure.Shared.Outbox.Abstractions;
using FarmManagement.Infrastructure.Shared.Persistence.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace FarmManagement.Infrastructure.Shared.Outbox.Processing;

public class OutboxProcessor
{
    private readonly IOutboxMessageRepository _outboxRepository;
    private readonly IEventPublisher _eventPublisher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly OutboxOptions _options;
    private readonly ILogger<OutboxProcessor> _logger;

    public OutboxProcessor(
        IOutboxMessageRepository outboxRepository,
        IEventPublisher eventPublisher,
        IUnitOfWork unitOfWork,
        IOptions<OutboxOptions> options,
        ILogger<OutboxProcessor> logger)
    {
        _outboxRepository = outboxRepository;
        _eventPublisher = eventPublisher;
        _unitOfWork = unitOfWork;
        _options = options.Value;
        _logger = logger;
    }

    public async Task ProcessPendingMessagesAsync(CancellationToken cancellationToken = default)
    {
        var pendingMessages = await _outboxRepository.GetPendingMessagesAsync(
            _options.BatchSize,
            cancellationToken);

        foreach (var message in pendingMessages)
        {
            try
            {
                await _outboxRepository.MarkAsProcessingAsync(message.Id, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                var eventType = Type.GetType(message.EventType);
                if (eventType == null)
                {
                    _logger.LogWarning(
                        "Event type {EventType} not found for outbox message {MessageId}",
                        message.EventType,
                        message.Id);
                    
                    await _outboxRepository.MarkAsFailedAsync(
                        message.Id,
                        $"Event type {message.EventType} not found",
                        cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    continue;
                }

                var integrationEvent = JsonSerializer.Deserialize(message.Payload, eventType);
                if (integrationEvent == null)
                {
                    _logger.LogWarning(
                        "Failed to deserialize payload for outbox message {MessageId}",
                        message.Id);
                    
                    await _outboxRepository.MarkAsFailedAsync(
                        message.Id,
                        "Failed to deserialize payload",
                        cancellationToken);
                    await _unitOfWork.SaveChangesAsync(cancellationToken);
                    continue;
                }

                await _eventPublisher.PublishAsync(integrationEvent, cancellationToken);

                await _outboxRepository.MarkAsProcessedAsync(message.Id, cancellationToken);
                await _unitOfWork.SaveChangesAsync(cancellationToken);

                _logger.LogInformation(
                    "Successfully processed outbox message {MessageId} of type {EventType}",
                    message.Id,
                    message.EventType);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error processing outbox message {MessageId}",
                    message.Id);

                await _outboxRepository.IncrementRetryCountAsync(message.Id, cancellationToken);
                var newRetryCount = message.RetryCount + 1;

                if (newRetryCount >= _options.MaxRetryAttempts)
                {
                    await _outboxRepository.MarkAsFailedAsync(
                        message.Id,
                        ex.Message,
                        cancellationToken);
                }
                else
                {
                    // Reset to pending for retry
                    await _outboxRepository.ResetToPendingAsync(message.Id, cancellationToken);
                }

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
        }
    }
}