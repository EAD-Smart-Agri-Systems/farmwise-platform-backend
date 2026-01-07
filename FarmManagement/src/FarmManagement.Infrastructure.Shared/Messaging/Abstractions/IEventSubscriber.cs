namespace FarmManagement.Infrastructure.Shared.Messaging.Abstractions;

public interface IEventSubscriber
{
    Task SubscribeAsync<T>(string eventName, Func<T, CancellationToken, Task> handler, CancellationToken cancellationToken = default) 
        where T : class;
    
    Task UnsubscribeAsync(string eventName, CancellationToken cancellationToken = default);
}

