namespace Crop.Domain.ValueObjects;

public sealed record FarmId(Guid Value)
{
    public static FarmId From(Guid value)
    {
        if (value == Guid.Empty)
            throw new ArgumentException("FarmId cannot be empty.");

        return new FarmId(value);
    }
}
