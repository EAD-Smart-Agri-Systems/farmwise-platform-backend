namespace FarmManagement.Infrastructure.Shared.Messaging.Events;

/// <summary>
/// Integration event representation of FarmRegistered domain event
/// Used for cross-module communication via RabbitMQ
/// </summary>
public record FarmRegisteredIntegrationEvent(
    Guid FarmId,
    string FarmName,
    decimal Latitude,
    decimal Longitude) : IntegrationEvent;
