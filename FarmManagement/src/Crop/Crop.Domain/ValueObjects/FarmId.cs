namespace Crop.Domain.ValueObjects;

public sealed class FarmId : IEquatable<FarmId>
{
    public Guid Value { get; }

    public FarmId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("FarmId cannot be empty.", nameof(value));

        Value = value;
    }

    public bool Equals(FarmId? other)
        => other is not null && Value.Equals(other.Value);

    public override bool Equals(object? obj)
        => Equals(obj as FarmId);

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();
}
