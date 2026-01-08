namespace FarmManagement.Modules.Crop.Api.Contracts;

public sealed record StartCropCycleRequest(
    Guid FarmId,
    Guid FieldId,
    int CropCode,
    string CropName,
    string TypicalStages,
    int DurationDays,
    DateTime PlantingDate
);
