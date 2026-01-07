namespace Crop.Domain.ValueObjects;

public sealed class FieldId : IEquatable<FieldId>
{
    public Guid Value { get; }

    public FieldId(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("FieldId cannot be empty.", nameof(value));

        Value = value;
    }

    public bool Equals(FieldId? other)
        => other is not null && Value.Equals(other.Value);

    public override bool Equals(object? obj)
        => Equals(obj as FieldId);

    public override int GetHashCode()
        => Value.GetHashCode();

    public override string ToString()
        => Value.ToString();
}
