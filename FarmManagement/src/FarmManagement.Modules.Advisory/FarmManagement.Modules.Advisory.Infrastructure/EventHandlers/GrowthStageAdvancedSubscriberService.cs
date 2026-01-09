using FarmManagement.Infrastructure.Shared.Messaging.Abstractions;
using FarmManagement.Infrastructure.Shared.Messaging.Events;
using FarmManagement.Modules.Advisory.Infrastructure.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FarmManagement.Modules.Advisory.Infrastructure.EventHandlers;

/// <summary>
/// Background service that subscribes to GrowthStageAdvanced integration events from the Crop module
/// This demonstrates inter-module communication via RabbitMQ
/// </summary>
public class GrowthStageAdvancedSubscriberService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventSubscriber _eventSubscriber;
    private readonly ILogger<GrowthStageAdvancedSubscriberService> _logger;

    public GrowthStageAdvancedSubscriberService(
        IServiceProvider serviceProvider,
        IEventSubscriber eventSubscriber,
        ILogger<GrowthStageAdvancedSubscriberService> logger)
    {
        _serviceProvider = serviceProvider;
        _eventSubscriber = eventSubscriber;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting GrowthStageAdvanced Event Subscriber Service");

        await _eventSubscriber.SubscribeAsync<GrowthStageAdvancedIntegrationEvent>(
            eventName: "GrowthStageAdvanced",
            handler: async (integrationEvent, cancellationToken) =>
            {
                using var scope = _serviceProvider.CreateScope();
                var eventHandler = scope.ServiceProvider
                    .GetRequiredService<GrowthStageAdvancedEventHandler>();
                
                await eventHandler.HandleAsync(integrationEvent, cancellationToken);
            },
            cancellationToken: stoppingToken);

        _logger.LogInformation("Subscribed to GrowthStageAdvanced events");

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping GrowthStageAdvanced Event Subscriber Service");
        await _eventSubscriber.UnsubscribeAsync("GrowthStageAdvanced", cancellationToken);
        await base.StopAsync(cancellationToken);
    }
}
