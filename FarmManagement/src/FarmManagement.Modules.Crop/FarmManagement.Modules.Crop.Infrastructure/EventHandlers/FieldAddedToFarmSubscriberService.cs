using FarmManagement.Infrastructure.Shared.Messaging.Abstractions;
using FarmManagement.Infrastructure.Shared.Messaging.Events;
using FarmManagement.Modules.Crop.Infrastructure.EventHandlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FarmManagement.Modules.Crop.Infrastructure.EventHandlers;

/// <summary>
/// Background service that subscribes to FieldAddedToFarm integration events from the Farm module
/// This demonstrates inter-module communication via RabbitMQ
/// </summary>
public class FieldAddedToFarmSubscriberService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IEventSubscriber _eventSubscriber;
    private readonly ILogger<FieldAddedToFarmSubscriberService> _logger;

    public FieldAddedToFarmSubscriberService(
        IServiceProvider serviceProvider,
        IEventSubscriber eventSubscriber,
        ILogger<FieldAddedToFarmSubscriberService> logger)
    {
        _serviceProvider = serviceProvider;
        _eventSubscriber = eventSubscriber;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Starting FieldAddedToFarm Event Subscriber Service");

        await _eventSubscriber.SubscribeAsync<FieldAddedToFarmIntegrationEvent>(
            eventName: "FieldAddedToFarm",
            handler: async (integrationEvent, cancellationToken) =>
            {
                using var scope = _serviceProvider.CreateScope();
                var eventHandler = scope.ServiceProvider
                    .GetRequiredService<FieldAddedToFarmEventHandler>();
                
                await eventHandler.HandleAsync(integrationEvent, cancellationToken);
            },
            cancellationToken: stoppingToken);

        _logger.LogInformation("Subscribed to FieldAddedToFarm events");

        await Task.Delay(Timeout.Infinite, stoppingToken);
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Stopping FieldAddedToFarm Event Subscriber Service");
        await _eventSubscriber.UnsubscribeAsync("FieldAddedToFarm", cancellationToken);
        await base.StopAsync(cancellationToken);
    }
}
