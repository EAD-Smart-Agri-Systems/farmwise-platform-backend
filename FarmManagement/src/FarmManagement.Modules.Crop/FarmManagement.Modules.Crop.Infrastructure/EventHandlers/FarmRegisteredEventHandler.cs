using FarmManagement.Infrastructure.Shared.Messaging.Events;
using Microsoft.Extensions.Logging;

namespace FarmManagement.Modules.Crop.Infrastructure.EventHandlers;

/// <summary>
/// Event handler that processes FarmRegistered events from the Farm module
/// Updates local read models or caches farm information for crop cycle validation
/// </summary>
public class FarmRegisteredEventHandler
{
    private readonly ILogger<FarmRegisteredEventHandler> _logger;

    public FarmRegisteredEventHandler(ILogger<FarmRegisteredEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(
        FarmRegisteredIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Received FarmRegistered event for FarmId: {FarmId}, Name: {FarmName}",
            integrationEvent.FarmId,
            integrationEvent.FarmName);

        try
        {
            // In a real scenario, you might:
            // 1. Update a read model with farm information
            // 2. Cache farm metadata for validation
            // 3. Initialize farm-specific crop cycle tracking
            
            _logger.LogInformation(
                "Successfully processed FarmRegistered event for FarmId: {FarmId}",
                integrationEvent.FarmId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error handling FarmRegistered event for FarmId: {FarmId}",
                integrationEvent.FarmId);
            throw; // Re-throw to trigger RabbitMQ retry mechanism
        }

        await Task.CompletedTask;
    }
}
