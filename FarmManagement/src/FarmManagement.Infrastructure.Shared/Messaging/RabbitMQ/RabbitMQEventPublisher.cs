using FarmManagement.Infrastructure.Shared.Configuration;
using FarmManagement.Infrastructure.Shared.Messaging.Abstractions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace FarmManagement.Infrastructure.Shared.Messaging.RabbitMQ;

public class RabbitMQEventPublisher : IEventPublisher, IDisposable
{
    private readonly RabbitMQConnectionFactory _connectionFactory;
    private readonly RabbitMQOptions _options;
    private readonly ILogger<RabbitMQEventPublisher> _logger;
    private IModel? _channel;

    public RabbitMQEventPublisher(
        RabbitMQConnectionFactory connectionFactory,
        IOptions<RabbitMQOptions> options,
        ILogger<RabbitMQEventPublisher> logger)
    {
        _connectionFactory = connectionFactory;
        _options = options.Value;
        _logger = logger;
    }

    private IModel Channel
    {
        get
        {
            if (_channel?.IsOpen == true)
            {
                return _channel;
            }

            _channel = _connectionFactory.CreateModel();
            _channel.ExchangeDeclare(
                exchange: _options.ExchangeName,
                type: _options.ExchangeType,
                durable: _options.Durable,
                autoDelete: _options.AutoDelete);

            return _channel;
        }
    }

    public async Task PublishAsync<T>(T integrationEvent, CancellationToken cancellationToken = default) 
        where T : class
    {
        try
        {
            var eventName = typeof(T).Name;
            var message = JsonSerializer.Serialize(integrationEvent);
            var body = Encoding.UTF8.GetBytes(message);

            var properties = Channel.CreateBasicProperties();
            properties.Persistent = true;
            properties.MessageId = Guid.NewGuid().ToString();
            properties.Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            properties.Type = eventName;

            Channel.BasicPublish(
                exchange: _options.ExchangeName,
                routingKey: eventName,
                basicProperties: properties,
                body: body);

            _logger.LogInformation(
                "Published integration event {EventName} with ID {EventId}",
                eventName,
                properties.MessageId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error publishing integration event {EventType}", typeof(T).Name);
            throw;
        }

        await Task.CompletedTask;
    }

    public void Dispose()
    {
        _channel?.Close();
        _channel?.Dispose();
    }
}