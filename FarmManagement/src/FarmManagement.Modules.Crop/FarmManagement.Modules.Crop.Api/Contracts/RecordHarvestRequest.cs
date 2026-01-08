namespace FarmManagement.Modules.Crop.Api.Contracts;

public sealed record RecordHarvestRequest(
    double Quantity,
    string Unit,
    DateTime HarvestDate
);