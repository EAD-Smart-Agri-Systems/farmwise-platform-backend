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
}
