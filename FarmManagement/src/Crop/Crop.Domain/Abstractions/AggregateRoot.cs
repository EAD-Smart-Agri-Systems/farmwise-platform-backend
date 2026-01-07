using System.Collections.ObjectModel;

namespace Crop.Domain.Abstractions;

public abstract class AggregateRoot
{
    private readonly List<DomainEvent> _domainEvents = new();

    public IReadOnlyCollection<DomainEvent> DomainEvents =>
        new ReadOnlyCollection<DomainEvent>(_domainEvents);

    protected void RaiseDomainEvent(DomainEvent domainEvent)
    {
        if (domainEvent is null)
            throw new ArgumentNullException(nameof(domainEvent));

        _domainEvents.Add(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
