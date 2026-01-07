namespace FarmManagement.Infrastructure.Shared.Configuration;

public class QuartzOptions
{
    public const string SectionName = "Quartz";

    public string ConnectionString { get; set; } = string.Empty;
    public bool UseInMemoryStore { get; set; } = false;
}

