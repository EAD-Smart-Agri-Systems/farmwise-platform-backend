using FarmManagement.Modules.Advisory.Domain.ValueObjects;

namespace FarmManagement.Modules.Advisory.Domain.Services;

public class AdvisoryRiskAssessmentService : IAdvisoryRiskAssessmentService
{
    public PestRiskLevel AssessRisk(string cropType, string weatherCondition)
    {
        return weatherCondition switch
        {
            "Rainy" => PestRiskLevel.High(),
            "Humid" => PestRiskLevel.Medium(),
            _ => PestRiskLevel.Low()
        };
    }
}
