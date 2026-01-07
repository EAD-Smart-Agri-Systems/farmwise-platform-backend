namespace FarmManagement.Infrastructure.Shared.Messaging.Abstractions;

public interface IEventPublisher
{
    Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default) 
        where T : class;
}

