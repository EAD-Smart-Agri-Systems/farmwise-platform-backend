namespace FarmManagement.Infrastructure.Shared.Background.Quartz;

public class QuartzJobConfig
{
    public string JobName { get; set; } = string.Empty;
    public string TriggerName { get; set; } = string.Empty;
    public int IntervalSeconds { get; set; } = 60;
    public bool RepeatForever { get; set; } = true;
}