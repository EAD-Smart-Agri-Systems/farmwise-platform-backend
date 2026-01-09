using FarmManagement.Modules.Crop.Domain.Enums;

namespace FarmManagement.Modules.Crop.Domain.ValueObjects;

public sealed class YieldRecord : IEquatable<YieldRecord>
{
    public float Quantity { get; private set; }
    public YieldUnit Unit { get; private set; }
    public DateTime HarvestDate { get; private set; }

    // Parameterless constructor for EF Core
    #pragma warning disable CS8618
    private YieldRecord() { }
    #pragma warning restore CS8618

    private YieldRecord(float quantity, YieldUnit unit, DateTime harvestDate)
    {
        if (quantity <= 0)
            throw new ArgumentException("Yield quantity must be greater than zero.", nameof(quantity));

        if (!Enum.IsDefined(typeof(YieldUnit), unit))
            throw new ArgumentException("Invalid yield unit.", nameof(unit));

        if (harvestDate > DateTime.UtcNow)
            throw new ArgumentException("Harvest date cannot be in the future.", nameof(harvestDate));

        Quantity = quantity;
        Unit = unit;
        HarvestDate = harvestDate;
    }

    public static YieldRecord Create(
        float quantity,
        YieldUnit unit,
        DateTime harvestDate)
    {
        return new YieldRecord(quantity, unit, harvestDate);
    }

    public bool Equals(YieldRecord? other)
        => other is not null &&
           Quantity.Equals(other.Quantity) &&
           Unit == other.Unit &&
           HarvestDate.Equals(other.HarvestDate);

    public override bool Equals(object? obj)
        => Equals(obj as YieldRecord);

    public override int GetHashCode()
        => HashCode.Combine(Quantity, Unit, HarvestDate);

    public override string ToString()
        => $"{Quantity} {Unit} harvested on {HarvestDate:yyyy-MM-dd}";
}
