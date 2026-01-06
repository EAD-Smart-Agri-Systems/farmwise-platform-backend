using FarmManagement.Modules.Advisory.Domain.ValueObjects;

namespace FarmManagement.Modules.Advisory.Application.Interfaces;

public interface IAdvisoryRiskAssessmentService
{
    PestRiskLevel AssessRisk(
        string cropType,
        double temperature,
        double humidity
    );
}
