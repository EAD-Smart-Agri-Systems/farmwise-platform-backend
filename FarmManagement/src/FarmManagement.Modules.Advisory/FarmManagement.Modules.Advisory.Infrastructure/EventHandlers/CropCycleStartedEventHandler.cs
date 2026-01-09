using FarmManagement.Infrastructure.Shared.Messaging.Abstractions;
using FarmManagement.Infrastructure.Shared.Messaging.Events;
using FarmManagement.Modules.Advisory.Application.Commands.GenerateAdvisoryReport;
using Microsoft.Extensions.Logging;

namespace FarmManagement.Modules.Advisory.Infrastructure.EventHandlers;

/// <summary>
/// Event handler that subscribes to CropCycleStarted events from the Crop module
/// and automatically generates advisory reports for new crop cycles.
/// </summary>
public class CropCycleStartedEventHandler
{
    private readonly GenerateAdvisoryReportHandler _advisoryHandler;
    private readonly ILogger<CropCycleStartedEventHandler> _logger;

    public CropCycleStartedEventHandler(
        GenerateAdvisoryReportHandler advisoryHandler,
        ILogger<CropCycleStartedEventHandler> logger)
    {
        _advisoryHandler = advisoryHandler;
        _logger = logger;
    }

    public async Task HandleAsync(CropCycleStartedIntegrationEvent integrationEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Received CropCycleStarted event for CropCycleId: {CropCycleId}, FarmId: {FarmId}",
            integrationEvent.CropCycleId,
            integrationEvent.FarmId);

        try
        {
            // Generate advisory report with default weather conditions
            // In a real scenario, you might fetch actual weather data from an external service
            var command = new GenerateAdvisoryReportCommand(
                FarmId: integrationEvent.FarmId,
                CropType: integrationEvent.CropTypeName,
                Temperature: 22.0, // Default temperature - in production, fetch from weather service
                Humidity: 65.0,   // Default humidity - in production, fetch from weather service
                WeatherCondition: "Normal",
                Recommendation: $"Initial advisory for {integrationEvent.CropTypeName} crop cycle. Monitor growth stages regularly."
            );

            var reportId = await _advisoryHandler.HandleAsync(command);
            
            _logger.LogInformation(
                "Successfully generated advisory report {ReportId} for CropCycle {CropCycleId}",
                reportId,
                integrationEvent.CropCycleId);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Error handling CropCycleStarted event for CropCycleId: {CropCycleId}",
                integrationEvent.CropCycleId);
            throw; // Re-throw to trigger RabbitMQ retry mechanism
        }
    }
}