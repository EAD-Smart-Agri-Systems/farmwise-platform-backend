namespace FarmManagement.Infrastructure.Shared.Messaging.Events;

/// <summary>
/// Integration event representation of CropCycleStarted domain event
/// Used for cross-module communication via RabbitMQ
/// </summary>
public record CropCycleStartedIntegrationEvent(
    Guid CropCycleId,
    Guid FarmId,
    Guid FieldId,
    string CropTypeName,
    int CropTypeDurationDays,
    DateTime StartDate,
    DateTime OccurredOn);