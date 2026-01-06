namespace FarmManagement.Modules.Advisory.Application.Commands.GenerateAdvisoryReport;

public sealed record GenerateAdvisoryReportCommand(
    Guid FarmId,
    string CropType,
    double Temperature,
    double Humidity,
    string WeatherCondition,
    string Recommendation
);
