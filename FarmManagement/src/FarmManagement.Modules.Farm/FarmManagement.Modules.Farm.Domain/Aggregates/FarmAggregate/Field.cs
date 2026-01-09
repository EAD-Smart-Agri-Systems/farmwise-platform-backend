using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;

public sealed class Field : Entity
{
    public FieldId Id { get; private set; }
    public string Name { get; private set; }

    private Field() { }

    public Field(FieldId id, string name)
    {
        Id = id;
        Name = name;
    }
}

// using FarmManagement.SharedKernel.Domain;

// namespace FarmManagement.Modules.Farm.Domain.Aggregates.FarmAggregate;

// public sealed class Field : Entity
// {
//     public FieldId Id { get; private set; }
//     public string Name { get; private set; }

//     private Field() { }

//     public Field(FieldId id, string name)
//         : base(id)
//     {
//         Name = name;
//     }
// }
