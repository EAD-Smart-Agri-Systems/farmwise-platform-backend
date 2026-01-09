using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Farm.Domain.Events;

public sealed record FieldAddedToFarm(FarmId FarmId, FieldId FieldId) : DomainEvent;


// using FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;

// namespace FarmManagement.Modules.Farm.Domain.Events;

// public record FieldAddedToFarm(FarmId FarmId, FieldId FieldId) : DomainEvent;
