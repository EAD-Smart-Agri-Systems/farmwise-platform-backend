namespace FarmManagement.SharedKernel.Domain;

public sealed class FarmId : ValueObject
{
    public Guid Value { get; }

    private FarmId(Guid value)
    {
        Value = value;
    }

    public static FarmId New()
        => new(Guid.NewGuid());

    public static FarmId From(Guid value)
        => new(value);

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value.ToString();
}
// namespace FarmManagement.SharedKernel.Domain;

// public sealed record FarmId(Guid Value)
// {
//     public static FarmId From(Guid value)
//     {
//         if (value == Guid.Empty)
//             throw new ArgumentException("FarmId cannot be empty.");

//         return new FarmId(value);
//     }
// }

// // namespace FarmManagement.Modules.Crop.Domain.ValueObjects;
// namespace FarmManagement.Modules.Farm.Domain.ValueObjects;

// public sealed record FarmId(Guid Value)
// {
//     public static FarmId From(Guid value)
//     {
//         if (value == Guid.Empty)
//             throw new ArgumentException("FarmId cannot be empty.");

//         return new FarmId(value);
//     }
// }
