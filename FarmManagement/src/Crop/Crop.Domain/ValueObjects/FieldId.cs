namespace Crop.Domain.ValueObjects;

public sealed record FieldId(Guid Value)
{
    public static FieldId From(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("FieldId cannot be empty.");

        return new FieldId(value);
    }
}
