using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Advisory.Domain.Events;

public record AdvisoryGeneratedEvent(
    Guid AdvisoryReportId,
    Guid FarmId
) : DomainEvent;
