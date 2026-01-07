namespace FarmManagement.Modules.Farm.Domain.ValueObjects;

public sealed record FarmId(Guid Value)
{
    public static FarmId From(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("FarmId cannot be empty.");

        return new FarmId(value);
    }
}

// namespace FarmManagement.Modules.Farm.Domain.ValueObjects;

// public sealed class FarmId : ValueObject
// {
//     public Guid Value { get; }

//     private FarmId(Guid value) => Value = value;

//     public static FarmId New() => new(Guid.NewGuid());

//     protected override IEnumerable<object> GetEqualityComponents()
//     {
//         yield return Value;
//     }
// }

// // public sealed record FarmId(Guid Value)
// // {
// //     public static FarmId New() => new(Guid.NewGuid());
// // }
