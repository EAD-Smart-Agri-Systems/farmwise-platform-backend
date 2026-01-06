using FarmManagement.SharedKernel.Domain;

namespace FarmManagement.Modules.Advisory.Domain.ValueObjects;

public class PestRiskLevel : ValueObject
{
    public string Level { get; }

    private PestRiskLevel(string level)
    {
        Level = level;
    }

    public static PestRiskLevel Low() => new("Low");
    public static PestRiskLevel Medium() => new("Medium");
    public static PestRiskLevel High() => new("High");

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Level;
    }
}
