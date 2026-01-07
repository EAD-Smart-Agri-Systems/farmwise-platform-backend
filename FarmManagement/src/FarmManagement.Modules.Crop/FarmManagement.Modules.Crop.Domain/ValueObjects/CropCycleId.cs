namespace FarmManagement.Modules.Crop.Domain.ValueObjects;

public sealed class CropCycleId : IEquatable<CropCycleId>
{
    public Guid Value { get; }

    private CropCycleId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("CropCycleId cannot be empty.", nameof(value));

        Value = value;
    }

    public static CropCycleId New()
        => new(Guid.NewGuid());

    public static CropCycleId From(Guid value)
        => new(value);

    public bool Equals(CropCycleId? other)
        => other is not null && Value.Equals(other.Value);

    public override bool Equals(object? obj)
        => Equals(obj as CropCycleId);

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();
}
