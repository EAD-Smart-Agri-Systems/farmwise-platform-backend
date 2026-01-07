using FarmManagement.Infrastructure.Shared.Configuration;
using FarmManagement.Infrastructure.Shared.Messaging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace FarmManagement.Infrastructure.Shared.Messaging.RabbitMQ;

public class RabbitMQEventSubscriber : IEventSubscriber, IDisposable
{
    private readonly RabbitMQConnectionFactory _connectionFactory;
    private readonly RabbitMQOptions _options;
    private readonly ILogger<RabbitMQEventSubscriber> _logger;
    private readonly Dictionary<string, IModel> _channels = new();
    private readonly Dictionary<string, EventingBasicConsumer> _consumers = new();

    public RabbitMQEventSubscriber(
        RabbitMQConnectionFactory connectionFactory,
        IOptions<RabbitMQOptions> options,
        ILogger<RabbitMQEventSubscriber> logger)
    {
        _connectionFactory = connectionFactory;
        _options = options.Value;
        _logger = logger;
    }

    public async Task SubscribeAsync<T>(
        string eventName,
        Func<T, CancellationToken, Task> handler,
        CancellationToken cancellationToken = default) 
        where T : class
    {
        try
        {
            var channel = _connectionFactory.CreateModel();
            channel.ExchangeDeclare(
                exchange: _options.ExchangeName,
                type: _options.ExchangeType,
                durable: _options.Durable,
                autoDelete: _options.AutoDelete);

            var queueName = $"{eventName}.queue";
            channel.QueueDeclare(
                queue: queueName,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueBind(
                queue: queueName,
                exchange: _options.ExchangeName,
                routingKey: eventName);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += async (model, ea) =>
            {
                try
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var integrationEvent = JsonSerializer.Deserialize<T>(message);

                    if (integrationEvent != null)
                    {
                        await handler(integrationEvent, cancellationToken);
                        channel.BasicAck(ea.DeliveryTag, false);
                        
                        _logger.LogInformation(
                            "Processed integration event {EventName} with delivery tag {DeliveryTag}",
                            eventName,
                            ea.DeliveryTag);
                    }
                    else
                    {
                        _logger.LogWarning("Failed to deserialize event {EventName}", eventName);
                        channel.BasicNack(ea.DeliveryTag, false, true);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing integration event {EventName}", eventName);
                    channel.BasicNack(ea.DeliveryTag, false, true);
                }
            };

            channel.BasicConsume(
                queue: queueName,
                autoAck: false,
                consumer: consumer);

            _channels[eventName] = channel;
            _consumers[eventName] = consumer;

            _logger.LogInformation("Subscribed to integration event {EventName}", eventName);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error subscribing to integration event {EventName}", eventName);
            throw;
        }

        await Task.CompletedTask;
    }

    public async Task UnsubscribeAsync(string eventName, CancellationToken cancellationToken = default)
    {
        if (_channels.TryGetValue(eventName, out var channel))
        {
            channel.Close();
            channel.Dispose();
            _channels.Remove(eventName);
            _consumers.Remove(eventName);
            
            _logger.LogInformation("Unsubscribed from integration event {EventName}", eventName);
        }

        await Task.CompletedTask;
    }

    public void Dispose()
    {
        foreach (var channel in _channels.Values)
        {
            channel.Close();
            channel.Dispose();
        }
        _channels.Clear();
        _consumers.Clear();
    }
}