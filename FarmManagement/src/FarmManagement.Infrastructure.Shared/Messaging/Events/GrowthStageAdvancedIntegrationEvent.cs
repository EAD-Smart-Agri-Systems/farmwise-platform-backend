namespace FarmManagement.Infrastructure.Shared.Messaging.Events;

/// <summary>
/// Integration event representation of GrowthStageAdvanced domain event
/// Used for cross-module communication via RabbitMQ
/// </summary>
public record GrowthStageAdvancedIntegrationEvent(
    Guid CropCycleId,
    string FromStage,
    string ToStage) : IntegrationEvent;
