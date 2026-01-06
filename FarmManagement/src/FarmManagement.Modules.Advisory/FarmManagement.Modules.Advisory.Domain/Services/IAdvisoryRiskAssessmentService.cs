using FarmManagement.Modules.Advisory.Domain.ValueObjects;

namespace FarmManagement.Modules.Advisory.Domain.Services;

public interface IAdvisoryRiskAssessmentService
{
    PestRiskLevel AssessRisk(string cropType, string weatherCondition);
}
