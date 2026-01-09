namespace FarmManagement.Infrastructure.Shared.Messaging.Events;

/// <summary>
/// Integration event representation of FieldAddedToFarm domain event
/// Used for cross-module communication via RabbitMQ
/// </summary>
public record FieldAddedToFarmIntegrationEvent(
    Guid FarmId,
    Guid FieldId,
    string FieldName) : IntegrationEvent;
