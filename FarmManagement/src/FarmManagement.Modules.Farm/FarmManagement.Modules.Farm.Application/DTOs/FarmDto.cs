namespace FarmManagement.Modules.Farm.Application.DTOs;

public sealed record FarmDto(
    Guid Id,
    string Name,
    string Country,
    string Region,
    int FieldCount
);
