using System;
using Crop.Domain.Abstractions;
using Crop.Domain.Enums;
using Crop.Domain.Events;
using Crop.Domain.ValueObjects;

namespace Crop.Domain.Aggregates;

public sealed class CropCycle : AggregateRoot
{
    public CropCycleId Id { get; private set; }
    public FarmId FarmId { get; private set; }
    public FieldId FieldId { get; private set; }
    public CropType CropType { get; private set; }
    public GrowthStage CurrentStage { get; private set; }
    public DateTime PlantingDate { get; private set; }
    public DateTime ExpectedHarvestDate { get; private set; }
    public CropCycleStatus Status { get; private set; }
    public YieldRecord? YieldRecord { get; private set; }

    #pragma warning disable CS8618
    private CropCycle() { } // EF Core
    #pragma warning restore CS8618
    private CropCycle(
        CropCycleId id,
        FarmId farmId,
        FieldId fieldId,
        CropType cropType,
        DateTime plantingDate)
    {
        Id = id;
        FarmId = farmId;
        FieldId = fieldId;
        CropType = cropType;
        PlantingDate = plantingDate;

        CurrentStage = GrowthStage.seed;
        Status = CropCycleStatus.started;
        ExpectedHarvestDate = CalculateExpectedHarvestDate();

        AddDomainEvent(
            CropCycleStarted.Create(
                Id,
                FarmId,
                FieldId,
                CropType,
                PlantingDate));
    }

    public static CropCycle Start(
        FarmId farmId,
        FieldId fieldId,
        CropType cropType,
        DateTime plantingDate)
    {
        return new CropCycle(
            CropCycleId.New(),
            farmId,
            fieldId,
            cropType,
            plantingDate);
    }

    public void AdvanceStage(GrowthStage newStage)
    {
        if (Status != CropCycleStatus.started)
            throw new InvalidOperationException("Crop cycle is not active.");

        var previousStage = CurrentStage;
        CurrentStage = newStage;

        AddDomainEvent(
            GrowthStageAdvanced.Create(
                Id,
                previousStage,
                newStage));
    }

    public void RecordHarvest(YieldRecord yieldRecord)
    {
        if (Status != CropCycleStatus.started)
            throw new InvalidOperationException("Crop cycle already harvested.");

        YieldRecord = yieldRecord;
        Status = CropCycleStatus.harvested;
        CurrentStage = GrowthStage.harvest;
    }

    private DateTime CalculateExpectedHarvestDate()
    {
        return PlantingDate.AddDays(CropType.DurationDays);
    }
}
