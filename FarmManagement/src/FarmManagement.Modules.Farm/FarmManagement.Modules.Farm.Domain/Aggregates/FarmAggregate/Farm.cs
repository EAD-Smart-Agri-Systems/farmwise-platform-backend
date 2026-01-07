using FarmManagement.SharedKernel.Domain;
using FarmManagement.Modules.Farm.Domain.ValueObjects;
using FarmManagement.Modules.Farm.Domain.Events;

namespace FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;

public sealed class Farm : AggregateRoot
{
    public new FarmId Id { get; private set; } // 'new' because AggregateRoot/Entity already has Id
    private readonly List<Field> _fields = new();

    public FarmName Name { get; private set; }
    public Location Location { get; private set; }

    public IReadOnlyCollection<Field> Fields => _fields.AsReadOnly();

    private Farm() { } // For ORM

    public Farm(FarmId id, FarmName name, Location location)
    {
        Id = id;
        Name = name;
        Location = location;

        RaiseDomainEvent(new FarmRegistered(Id));
    }

    public void UpdateLocation(Location newLocation)
    {
        Location = newLocation;
        RaiseDomainEvent(new FarmLocationUpdated(Id, newLocation));
    }

    public void AddField(Field field)
    {
        _fields.Add(field);
        RaiseDomainEvent(new FieldAddedToFarm(Id, field.Id));
    }
}

// using FarmManagement.SharedKernel.Domain;
// using FarmManagement.Modules.Farm.Domain.ValueObjects;
// using FarmManagement.Modules.Farm.Domain.Events;

// namespace FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;

// public sealed class Farm : AggregateRoot
// {
//     public FarmId Id { get; private set; }
//     private readonly List<Field> _fields = new();

//     public FarmName Name { get; private set; }
//     public Location Location { get; private set; }

//     public IReadOnlyCollection<Field> Fields => _fields.AsReadOnly();

//     private Farm() { } // For ORM

//     public Farm(FarmId id, FarmName name, Location location)
//         : base(id)
//     {
//         Name = name;
//         Location = location;

//         // AddDomainEvent(new FarmRegistered(id));
//         RaiseDomainEvent(new FarmRegistered(Id));

//     }

//     public void UpdateLocation(Location newLocation)
//     {
//         Location = newLocation;
//         RaiseDomainEvent(new FarmLocationUpdated(Id, newLocation));
//     }

//     public void AddField(Field field)
//     {
//         _fields.Add(field);
//         RaiseDomainEvent(new FieldAddedToFarm(Id, field.Id));
//     }
// }
