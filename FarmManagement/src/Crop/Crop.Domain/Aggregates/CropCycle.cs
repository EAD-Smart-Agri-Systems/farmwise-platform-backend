using Crop.Domain.Abstractions;
using Crop.Domain.Enums;
using Crop.Domain.Events;
using Crop.Domain.ValueObjects;

namespace Crop.Domain.Aggregates;

public class CropCycle : AggregateRoot
{
    // EF Core constructor
    private CropCycle() { }

    // Real constructor (fully initializes the aggregate)
    private CropCycle(
        CropCycleId id,
        FarmId farmId,
        FieldId fieldId,
        CropType cropType)
    {
        Id = id;
        FarmId = farmId;
        FieldId = fieldId;
        CropType = cropType;

        GrowthStage = GrowthStage.Planting;
        Status = CropCycleStatus.Started;
    }

    public CropCycleId Id { get; private set; }
    public FarmId FarmId { get; private set; }
    public FieldId FieldId { get; private set; }
    public CropType CropType { get; private set; }

    public GrowthStage GrowthStage { get; private set; }
    public CropCycleStatus Status { get; private set; }

    public YieldRecord? Yield { get; private set; }

    // Factory method
    public static CropCycle Start(
        CropCycleId id,
        FarmId farmId,
        FieldId fieldId,
        CropType cropType)
    {
        var cropCycle = new CropCycle(id, farmId, fieldId, cropType);

        cropCycle.AddDomainEvent(
            new CropCycleStarted(id, farmId, fieldId, cropType)
        );

        return cropCycle;
    }

    public void AdvanceGrowthStage()
    {
        if (Status != CropCycleStatus.Started)
            throw new InvalidOperationException("Cannot advance growth stage for inactive crop cycle.");

        if (GrowthStage == GrowthStage.Harvested)
            throw new InvalidOperationException("Crop cycle is already harvested.");

        GrowthStage++;

        AddDomainEvent(
            new GrowthStageAdvanced(Id, GrowthStage)
        );
    }

    public void RecordHarvest(YieldRecord yield)
    {
        if (GrowthStage != GrowthStage.Harvested)
            throw new InvalidOperationException("Harvest can only be recorded at harvested stage.");

        Yield = yield;
        Status = CropCycleStatus.Completed;
    }
}
