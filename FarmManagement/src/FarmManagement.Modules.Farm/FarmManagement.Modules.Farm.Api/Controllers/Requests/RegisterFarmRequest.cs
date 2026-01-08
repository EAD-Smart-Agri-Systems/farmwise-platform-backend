namespace FarmManagement.Modules.Farm.Api.Controllers;

public sealed record RegisterFarmRequest(
    string Name,
    decimal Latitude,
    decimal Longitude);
