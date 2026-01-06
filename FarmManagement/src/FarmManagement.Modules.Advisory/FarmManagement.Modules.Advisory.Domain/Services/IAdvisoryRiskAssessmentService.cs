using FarmManagement.Modules.Advisory.Domain.ValueObjects;

namespace FarmManagement.Modules.Advisory.Domain.Services;

public interface IAdvisoryRiskAssessmentService
{
    PestRiskLevel AssessPestRisk(
        double temperature,
        double humidity,
        bool recentPestSightings);
}
