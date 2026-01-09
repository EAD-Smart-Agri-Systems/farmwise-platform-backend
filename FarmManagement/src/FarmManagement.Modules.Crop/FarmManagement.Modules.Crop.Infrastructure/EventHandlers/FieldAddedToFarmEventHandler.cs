using FarmManagement.Infrastructure.Shared.Messaging.Events;
using Microsoft.Extensions.Logging;

namespace FarmManagement.Modules.Crop.Infrastructure.EventHandlers;

/// <summary>
/// Event handler that processes FieldAddedToFarm events from the Farm module
/// Updates field inventory for crop cycle planning
/// </summary>
public class FieldAddedToFarmEventHandler
{
    private readonly ILogger<FieldAddedToFarmEventHandler> _logger;

    public FieldAddedToFarmEventHandler(ILogger<FieldAddedToFarmEventHandler> logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(
        FieldAddedToFarmIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Received FieldAddedToFarm event for FarmId: {FarmId}, FieldId: {FieldId}, FieldName: {FieldName}",
            integrationEvent.FarmId,
            integrationEvent.FieldId,
            integrationEvent.FieldName);

        try
        {
            // In a real scenario, you might:
            // 1. Update a read model with field information
            // 2. Cache field metadata for crop cycle validation
            // 3. Track available fields for crop planning
            
            _logger.LogInformation(
                "Successfully processed FieldAddedToFarm event for FieldId: {FieldId}",
                integrationEvent.FieldId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error handling FieldAddedToFarm event for FieldId: {FieldId}",
                integrationEvent.FieldId);
            throw; // Re-throw to trigger RabbitMQ retry mechanism
        }

        await Task.CompletedTask;
    }
}
