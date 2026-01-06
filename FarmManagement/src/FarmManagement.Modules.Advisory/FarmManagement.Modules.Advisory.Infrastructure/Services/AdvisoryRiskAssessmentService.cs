using FarmManagement.Modules.Advisory.Application.Interfaces;
using FarmManagement.Modules.Advisory.Domain.ValueObjects;

namespace FarmManagement.Modules.Advisory.Infrastructure.Services;

public class AdvisoryRiskAssessmentService : IAdvisoryRiskAssessmentService
{
    public PestRiskLevel AssessRisk(
        string cropType,
        double temperature,
        double humidity)
    {
        // simple rule-based logic (can evolve later)
        if (cropType.Equals("maize", StringComparison.OrdinalIgnoreCase)
            && temperature > 25
            && humidity > 70)
        {
            return PestRiskLevel.High();
        }

        if (humidity > 60)
        {
            return PestRiskLevel.Medium();
        }

        return PestRiskLevel.Low();
    }
}
