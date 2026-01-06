using FarmManagement.Modules.Advisory.Domain.Aggregates;

namespace FarmManagement.Modules.Advisory.Application.Interfaces;

public interface IAdvisoryReportRepository
{
    Task AddAsync(AdvisoryReport report);
    Task<AdvisoryReport?> GetLatestByFarmIdAsync(Guid farmId);
}
