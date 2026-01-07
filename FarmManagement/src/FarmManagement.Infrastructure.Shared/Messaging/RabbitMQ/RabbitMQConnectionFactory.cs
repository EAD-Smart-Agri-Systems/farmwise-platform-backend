using FarmManagement.Infrastructure.Shared.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace FarmManagement.Infrastructure.Shared.Messaging.RabbitMQ;

public class RabbitMQConnectionFactory
{
    private readonly RabbitMQOptions _options;
    private IConnection? _connection;
    private readonly object _lock = new();

    public RabbitMQConnectionFactory(IOptions<RabbitMQOptions> options)
    {
        _options = options.Value;
    }

    public IConnection GetConnection()
    {
        if (_connection?.IsOpen == true)
        {
            return _connection;
        }

        lock (_lock)
        {
            if (_connection?.IsOpen == true)
            {
                return _connection;
            }

            var factory = new ConnectionFactory
            {
                HostName = _options.HostName,
                Port = _options.Port,
                UserName = _options.UserName,
                Password = _options.Password,
                VirtualHost = _options.VirtualHost,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(10)
            };

            _connection = factory.CreateConnection();
            return _connection;
        }
    }

    public IModel CreateModel()
    {
        var connection = GetConnection();
        return connection.CreateModel();
    }

    public void Dispose()
    {
        _connection?.Close();
        _connection?.Dispose();
    }
}

