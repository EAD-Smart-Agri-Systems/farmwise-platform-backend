using FarmManagement.Infrastructure.Shared.Messaging.Abstractions;
using FarmManagement.Infrastructure.Shared.Messaging.Events;
using FarmManagement.Modules.Advisory.Infrastructure.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FarmManagement.Modules.Advisory.Infrastructure.EventHandlers;

/// <summary>
/// Background service that subscribes to integration events from other modules
/// This demonstrates inter-module communication via RabbitMQ
/// </summary>
public class AdvisoryEventSubscriberService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventSubscriber _eventSubscriber;
    private readonly ILogger<AdvisoryEventSubscriberService> _logger;

    public AdvisoryEventSubscriberService(
        IServiceProvider serviceProvider,
        IEventSubscriber eventSubscriber,
        ILogger<AdvisoryEventSubscriberService> logger)
    {
        _serviceProvider = serviceProvider;
        _eventSubscriber = eventSubscriber;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting Advisory Event Subscriber Service");

        // Subscribe to CropCycleStarted events from Crop module
        await _eventSubscriber.SubscribeAsync<CropCycleStartedIntegrationEvent>(
            eventName: "CropCycleStarted",
            handler: async (integrationEvent, cancellationToken) =>
            {
                // Create a scope for this event handling
                using var scope = _serviceProvider.CreateScope();
                var eventHandler = scope.ServiceProvider.GetRequiredService<CropCycleStartedEventHandler>();
                
                await eventHandler.HandleAsync(integrationEvent, cancellationToken);
            },
            cancellationToken: stoppingToken);

        _logger.LogInformation("Subscribed to CropCycleStarted events");

        // Keep the service running
        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping Advisory Event Subscriber Service");
        await _eventSubscriber.UnsubscribeAsync("CropCycleStarted", cancellationToken);
        await base.StopAsync(cancellationToken);
    }
}