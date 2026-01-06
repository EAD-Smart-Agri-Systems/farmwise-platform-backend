using FarmManagement.Modules.Advisory.Domain.ValueObjects;

namespace FarmManagement.Modules.Advisory.Domain.Services;

public class AdvisoryRiskAssessmentService : IAdvisoryRiskAssessmentService
{
    public PestRiskLevel AssessPestRisk(
        double temperature,
        double humidity,
        bool recentPestSightings)
    {
        if (recentPestSightings || (temperature > 30 && humidity > 70))
            return PestRiskLevel.High();

        if (temperature > 25)
            return PestRiskLevel.Medium();

        return PestRiskLevel.Low();
    }
}
