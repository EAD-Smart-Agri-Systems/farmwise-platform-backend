namespace FarmManagement.Modules.Advisory.Api.Contracts;

public sealed record GenerateAdvisoryReportRequest(
    Guid FarmId,
    string CropType,
    double Temperature,
    double Humidity,
    string WeatherCondition,
    string Recommendation
);
