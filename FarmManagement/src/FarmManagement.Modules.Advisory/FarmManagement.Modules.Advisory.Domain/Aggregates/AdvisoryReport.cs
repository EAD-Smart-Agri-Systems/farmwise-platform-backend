using FarmManagement.SharedKernel.Domain;
using FarmManagement.Modules.Advisory.Domain.ValueObjects;
using FarmManagement.Modules.Advisory.Domain.Events;

namespace FarmManagement.Modules.Advisory.Domain.Aggregates;

public class AdvisoryReport : AggregateRoot
{
    public Guid FarmId { get; private set; }
    public Guid CropCycleId { get; private set; }

    public PestRiskLevel PestRisk { get; private set; }
    public string Recommendation { get; private set; }

    public DateTime GeneratedOn { get; private set; }

    private AdvisoryReport() { } // EF / serialization

    public AdvisoryReport(
        Guid farmId,
        Guid cropCycleId,
        PestRiskLevel pestRisk,
        string recommendation)
    {
        FarmId = farmId;
        CropCycleId = cropCycleId;
        PestRisk = pestRisk;
        Recommendation = recommendation;
        GeneratedOn = DateTime.UtcNow;

        RaiseDomainEvent(new AdvisoryGeneratedEvent(Id, FarmId));
    }
}
