using FarmManagement.SharedKernel.Domain;
using FarmManagement.Modules.Advisory.Domain.ValueObjects;

namespace FarmManagement.Modules.Advisory.Domain.Aggregates;

public class AdvisoryReport : AggregateRoot
{
    public Guid FarmId { get; private set; }
    public string CropType { get; private set; } = default!;
    public PestRiskLevel PestRiskLevel { get; private set; } = default!;
    public string Recommendation { get; private set; } = default!;
    public DateTime GeneratedOn { get; private set; }

    private AdvisoryReport() { }

    private AdvisoryReport(
        Guid farmId,
        string cropType,
        PestRiskLevel pestRiskLevel,
        string recommendation)
    {
        Id = Guid.NewGuid();
        FarmId = farmId;
        CropType = cropType;
        PestRiskLevel = pestRiskLevel;
        Recommendation = recommendation;
        GeneratedOn = DateTime.UtcNow;
    }

    public static AdvisoryReport Generate(
        Guid farmId,
        string cropType,
        PestRiskLevel pestRiskLevel,
        string recommendation)
    {
        return new AdvisoryReport(
            farmId,
            cropType,
            pestRiskLevel,
            recommendation);
    }
}
