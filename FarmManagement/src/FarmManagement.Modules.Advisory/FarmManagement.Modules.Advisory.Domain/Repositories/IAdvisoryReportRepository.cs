using FarmManagement.Modules.Advisory.Domain.Aggregates;

namespace FarmManagement.Modules.Advisory.Domain.Repositories;
public interface IAdvisoryReportRepository
{
    Task AddAsync(AdvisoryReport advisoryReport);
    Task<AdvisoryReport?> GetByIdAsync(Guid id);
}
