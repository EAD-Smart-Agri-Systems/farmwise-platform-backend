using FarmManagement.Infrastructure.Shared.Outbox.Processing;
using Microsoft.Extensions.Logging;
using Quartz;

namespace FarmManagement.Infrastructure.Shared.Background.Jobs;

[DisallowConcurrentExecution]
public class OutboxProcessorJob : IJob
{
    private readonly OutboxProcessor _outboxProcessor;
    private readonly ILogger<OutboxProcessorJob> _logger;

    public OutboxProcessorJob(
        OutboxProcessor outboxProcessor,
        ILogger<OutboxProcessorJob> logger)
    {
        _outboxProcessor = outboxProcessor;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("Starting outbox message processing job");

        try
        {
            await _outboxProcessor.ProcessPendingMessagesAsync(context.CancellationToken);
            _logger.LogInformation("Completed outbox message processing job");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error executing outbox message processing job");
            throw;
        }
    }
}