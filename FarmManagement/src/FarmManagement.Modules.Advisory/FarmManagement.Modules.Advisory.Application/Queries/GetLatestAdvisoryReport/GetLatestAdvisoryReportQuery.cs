namespace FarmManagement.Modules.Advisory.Application.Queries.GetLatestAdvisoryReport;

public class GetLatestAdvisoryReportQuery(Guid farmId)
{
    public Guid FarmId { get; } = farmId;
}
