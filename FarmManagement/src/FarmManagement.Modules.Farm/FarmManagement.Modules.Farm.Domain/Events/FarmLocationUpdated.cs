using FarmManagement.SharedKernel.Domain;
using FarmManagement.Modules.Farm.Domain.ValueObjects;

namespace FarmManagement.Modules.Farm.Domain.Events;

public sealed record FarmLocationUpdated(FarmId FarmId, Location Location) : DomainEvent;
