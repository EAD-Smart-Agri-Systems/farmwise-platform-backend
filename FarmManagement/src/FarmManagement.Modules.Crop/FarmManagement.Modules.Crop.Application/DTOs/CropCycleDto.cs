namespace FarmManagement.Modules.Crop.Application.DTOs
{
    public sealed record CropCycleDto(
        Guid Id,
        Guid FarmId,
        Guid FieldId,
        string CropType,
        string GrowthStage,
        string Status,
        DateTime StartDate,
        DateTime ExpectedHarvestDate,
        float? HarvestedQuantity,
        string? HarvestUnit
    );
}