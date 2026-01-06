using FarmManagement.Modules.Advisory.Domain.Aggregates;
using FarmManagement.Modules.Advisory.Application.Interfaces;


namespace FarmManagement.Modules.Advisory.Application.Commands.GenerateAdvisoryReport;

public class GenerateAdvisoryReportHandler
{
    private readonly IAdvisoryReportRepository _repository;
    private readonly IAdvisoryRiskAssessmentService _riskService;

    public GenerateAdvisoryReportHandler(
        IAdvisoryReportRepository repository,
        IAdvisoryRiskAssessmentService riskService)
    {
        _repository = repository;
        _riskService = riskService;
    }

    public async Task<Guid> HandleAsync(GenerateAdvisoryReportCommand command)
    {
        var pestRiskLevel = _riskService.AssessRisk(
            command.CropType,
            command.Temperature,
            command.Humidity);

        var report = AdvisoryReport.Generate(
            command.FarmId,
            command.CropType,
            pestRiskLevel,
            command.Recommendation);

        await _repository.AddAsync(report);

        return report.Id;
    }
}
