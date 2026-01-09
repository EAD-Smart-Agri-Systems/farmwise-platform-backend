using FarmManagement.Infrastructure.Shared.Messaging.Events;
using FarmManagement.Modules.Advisory.Application.Commands.GenerateAdvisoryReport;
using Microsoft.Extensions.Logging;

namespace FarmManagement.Modules.Advisory.Infrastructure.EventHandlers;

/// <summary>
/// Event handler that processes GrowthStageAdvanced events from the Crop module
/// and generates advisory recommendations based on growth stage changes
/// </summary>
public class GrowthStageAdvancedEventHandler
{
    private readonly GenerateAdvisoryReportHandler _advisoryHandler;
    private readonly ILogger<GrowthStageAdvancedEventHandler> _logger;

    public GrowthStageAdvancedEventHandler(
        GenerateAdvisoryReportHandler advisoryHandler,
        ILogger<GrowthStageAdvancedEventHandler> logger)
    {
        _advisoryHandler = advisoryHandler;
        _logger = logger;
    }

    public async Task HandleAsync(
        GrowthStageAdvancedIntegrationEvent integrationEvent,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Received GrowthStageAdvanced event for CropCycleId: {CropCycleId}, From: {FromStage}, To: {ToStage}",
            integrationEvent.CropCycleId,
            integrationEvent.FromStage,
            integrationEvent.ToStage);

        try
        {
            // Generate advisory report based on growth stage advancement
            // In a real scenario, you might:
            // 1. Fetch crop cycle details to get FarmId and CropType
            // 2. Fetch current weather conditions
            // 3. Generate stage-specific recommendations
            
            var command = new GenerateAdvisoryReportCommand(
                FarmId: Guid.Empty, // TODO: Fetch from crop cycle
                CropType: integrationEvent.ToStage,
                Temperature: 22.0, // Default - fetch from weather service
                Humidity: 65.0,     // Default - fetch from weather service
                WeatherCondition: "Normal",
                Recommendation: $"Crop has advanced from {integrationEvent.FromStage} to {integrationEvent.ToStage}. Monitor growth conditions closely."
            );

            var reportId = await _advisoryHandler.HandleAsync(command);
            
            _logger.LogInformation(
                "Successfully generated advisory report {ReportId} for GrowthStageAdvanced event, CropCycleId: {CropCycleId}",
                reportId,
                integrationEvent.CropCycleId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error handling GrowthStageAdvanced event for CropCycleId: {CropCycleId}",
                integrationEvent.CropCycleId);
            throw; // Re-throw to trigger RabbitMQ retry mechanism
        }
    }
}
