using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Farm.Domain.ValueObjects;

public sealed class FieldId : ValueObject
{
    public Guid Value { get; }

    private FieldId(Guid value) => Value = value;

    public static FieldId New() => new(Guid.NewGuid());

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}


// namespace FarmManagement.Modules.Farm.Domain.ValueObjects;

// public sealed class FieldId : ValueObject
// {
//     public Guid Value { get; }

//     private FieldId(Guid value) => Value = value;

//     public static FieldId New() => new(Guid.NewGuid());

//     protected override IEnumerable<object> GetEqualityComponents()
//     {
//         yield return Value;
//     }
// }

// // public sealed record FieldId(Guid Value)
// // {
// //     public static FieldId New() => new(Guid.NewGuid());
// // }
