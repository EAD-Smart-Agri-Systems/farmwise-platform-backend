namespace FarmManagement.Modules.Advisory.Application.Commands.GenerateAdvisoryReport;

public class GenerateAdvisoryReportCommand
{
    public Guid FarmId { get; init; }
    public string CropType { get; init; } = default!;
    public string WeatherCondition { get; init; } = default!;
    public string Recommendation { get; init; } = default!;
}
