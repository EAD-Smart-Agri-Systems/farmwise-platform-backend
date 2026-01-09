using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Farm.Domain.Events;

public sealed record FarmRegistered(FarmId FarmId) : DomainEvent;
