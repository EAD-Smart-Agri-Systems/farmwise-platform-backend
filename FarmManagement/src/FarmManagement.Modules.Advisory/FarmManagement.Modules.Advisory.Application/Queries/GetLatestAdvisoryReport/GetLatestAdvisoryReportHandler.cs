using FarmManagement.Modules.Advisory.Application.DTOs;
using FarmManagement.Modules.Advisory.Application.Interfaces;

namespace FarmManagement.Modules.Advisory.Application.Queries.GetLatestAdvisoryReport;

public class GetLatestAdvisoryReportHandler
{
    private readonly IAdvisoryReportRepository _repository;

    public GetLatestAdvisoryReportHandler(IAdvisoryReportRepository repository)
    {
        _repository = repository;
    }

    public async Task<AdvisoryReportDto?> HandleAsync(GetLatestAdvisoryReportQuery query)
    {
        var report = await _repository.GetLatestByFarmIdAsync(query.FarmId);

        if (report is null)
            return null;

        return new AdvisoryReportDto
        {
            Id = report.Id,
            FarmId = report.FarmId,
            CropType = report.CropType,
            PestRiskLevel = report.PestRiskLevel.Level,
            Recommendation = report.Recommendation,
            GeneratedOn = report.GeneratedOn
        };
    }
}
