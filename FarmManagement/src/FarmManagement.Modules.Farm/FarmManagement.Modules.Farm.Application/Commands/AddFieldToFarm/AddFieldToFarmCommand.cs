using FarmManagement.Modules.Farm.Domain.ValueObjects;

namespace FarmManagement.Modules.Farm.Application.Commands.AddFieldToFarm;

public sealed record AddFieldToFarmCommand(
    Guid FarmId,
    string FieldName
);
