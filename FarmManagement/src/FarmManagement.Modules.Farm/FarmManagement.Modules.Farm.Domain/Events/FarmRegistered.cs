using FarmManagement.SharedKernel.Domain;
using FarmManagement.Modules.Farm.Domain.ValueObjects;

namespace FarmManagement.Modules.Farm.Domain.Events;

public sealed record FarmRegistered(FarmId FarmId) : DomainEvent;
