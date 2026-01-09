using System.Text.Json;

namespace FarmManagement.Infrastructure.Shared.Messaging.Events;

/// <summary>
/// Maps domain events to integration events for cross-module communication
/// </summary>
public static class IntegrationEventMapper
{
    /// <summary>
    /// Creates an integration event from a deserialized domain event
    /// This is used when processing events from the outbox
    /// </summary>
    public static object? CreateIntegrationEvent(string eventType, string payload)
    {
        return eventType switch
        {
            var type when type.Contains("CropCycleStarted") => 
                CreateCropCycleStartedIntegrationEvent(payload),
            var type when type.Contains("FarmRegistered") => 
                CreateFarmRegisteredIntegrationEvent(payload),
            var type when type.Contains("FieldAddedToFarm") => 
                CreateFieldAddedToFarmIntegrationEvent(payload),
            var type when type.Contains("GrowthStageAdvanced") => 
                CreateGrowthStageAdvancedIntegrationEvent(payload),
            _ => null
        };
    }

    private static CropCycleStartedIntegrationEvent? CreateCropCycleStartedIntegrationEvent(string payload)
    {
        try
        {
            // Parse the JSON to extract needed fields
            using var doc = JsonDocument.Parse(payload);
            var root = doc.RootElement;

            // Extract values from the domain event JSON
            var cropCycleId = root.GetProperty("CropCycleId").GetProperty("Value").GetGuid();
            var farmId = root.GetProperty("FarmId").GetProperty("Value").GetGuid();
            var fieldId = root.GetProperty("FieldId").GetProperty("Value").GetGuid();
            var cropTypeName = root.GetProperty("CropType").GetProperty("Name").GetString() ?? string.Empty;
            var cropTypeDurationDays = root.GetProperty("CropType").GetProperty("DurationDays").GetInt32();
            var startDate = root.GetProperty("StartDate").GetDateTime();
            var occurredOn = root.TryGetProperty("OccurredOn", out var occurredOnProp) 
                ? occurredOnProp.GetDateTime() 
                : DateTime.UtcNow;

            return new CropCycleStartedIntegrationEvent(
                CropCycleId: cropCycleId,
                FarmId: farmId,
                FieldId: fieldId,
                CropTypeName: cropTypeName,
                CropTypeDurationDays: cropTypeDurationDays,
                StartDate: startDate,
                OccurredOn: occurredOn
            );
        }
        catch
        {
            return null;
        }
    }

    private static FarmRegisteredIntegrationEvent? CreateFarmRegisteredIntegrationEvent(string payload)
    {
        try
        {
            using var doc = JsonDocument.Parse(payload);
            var root = doc.RootElement;

            var farmId = root.GetProperty("FarmId").GetProperty("Value").GetGuid();
            var farmName = root.GetProperty("FarmId").GetProperty("Value").ToString(); // Fallback, may need adjustment
            var occurredOn = root.TryGetProperty("OccurredOn", out var occurredOnProp) 
                ? occurredOnProp.GetDateTime() 
                : DateTime.UtcNow;

            // Note: FarmRegistered domain event may not have all fields
            // This is a simplified mapping - adjust based on actual domain event structure
            return new FarmRegisteredIntegrationEvent(
                FarmId: farmId,
                FarmName: farmName,
                Latitude: 0, // TODO: Extract from domain event if available
                Longitude: 0 // TODO: Extract from domain event if available
            ) { OccurredOn = occurredOn };
        }
        catch
        {
            return null;
        }
    }

    private static FieldAddedToFarmIntegrationEvent? CreateFieldAddedToFarmIntegrationEvent(string payload)
    {
        try
        {
            using var doc = JsonDocument.Parse(payload);
            var root = doc.RootElement;

            var farmId = root.GetProperty("FarmId").GetProperty("Value").GetGuid();
            var fieldId = root.GetProperty("FieldId").GetProperty("Value").GetGuid();
            var occurredOn = root.TryGetProperty("OccurredOn", out var occurredOnProp) 
                ? occurredOnProp.GetDateTime() 
                : DateTime.UtcNow;

            return new FieldAddedToFarmIntegrationEvent(
                FarmId: farmId,
                FieldId: fieldId,
                FieldName: string.Empty // TODO: Extract from domain event if available
            ) { OccurredOn = occurredOn };
        }
        catch
        {
            return null;
        }
    }

    private static GrowthStageAdvancedIntegrationEvent? CreateGrowthStageAdvancedIntegrationEvent(string payload)
    {
        try
        {
            using var doc = JsonDocument.Parse(payload);
            var root = doc.RootElement;

            var cropCycleId = root.GetProperty("CropCycleId").GetProperty("Value").GetGuid();
            var fromStage = root.GetProperty("FromStage").ToString();
            var toStage = root.GetProperty("ToStage").ToString();
            var occurredOn = root.TryGetProperty("OccurredOn", out var occurredOnProp) 
                ? occurredOnProp.GetDateTime() 
                : DateTime.UtcNow;

            return new GrowthStageAdvancedIntegrationEvent(
                CropCycleId: cropCycleId,
                FromStage: fromStage,
                ToStage: toStage
            ) { OccurredOn = occurredOn };
        }
        catch
        {
            return null;
        }
    }
}
