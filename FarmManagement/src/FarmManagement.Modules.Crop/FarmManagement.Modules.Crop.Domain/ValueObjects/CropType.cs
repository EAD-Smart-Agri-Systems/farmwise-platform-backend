namespace FarmManagement.Modules.Crop.Domain.ValueObjects;

public sealed class CropType : IEquatable<CropType>
{
    public int CropCode { get; }
    public string Name { get; }
    public string TypicalStages { get; }
    public int DurationDays { get; }

    private CropType(int cropCode, string name, string typicalStages, int durationDays)
    {
        if (cropCode <= 0)
            throw new ArgumentException("CropCode must be positive.", nameof(cropCode));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Crop name is required.", nameof(name));

        if (string.IsNullOrWhiteSpace(typicalStages))
            throw new ArgumentException("Typical stages are required.", nameof(typicalStages));

        if (durationDays <= 0)
            throw new ArgumentException("Duration must be greater than zero.", nameof(durationDays));

        CropCode = cropCode;
        Name = name.Trim();
        TypicalStages = typicalStages.Trim();
        DurationDays = durationDays;
    }

    public static CropType Create(
        int cropCode,
        string name,
        string typicalStages,
        int durationDays)
    {
        return new CropType(cropCode, name, typicalStages, durationDays);
    }

    public bool Equals(CropType? other)
        => other is not null &&
           CropCode == other.CropCode &&
           Name == other.Name &&
           DurationDays == other.DurationDays;

    public override bool Equals(object? obj)
        => Equals(obj as CropType);

    public override int GetHashCode()
        => HashCode.Combine(CropCode, Name, DurationDays);

    public override string ToString()
        => $"{Name} ({DurationDays} days)";
}
