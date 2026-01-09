using FarmManagement.Modules.Advisory.Application.Interfaces;
using FarmManagement.Modules.Advisory.Domain.Aggregates;
using FarmManagement.Modules.Advisory.Infrastructure.Persistence;
using FarmManagement.Infrastructure.Shared.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace FarmManagement.Modules.Advisory.Infrastructure.Repositories;

public class AdvisoryReportRepository : IAdvisoryReportRepository
{
    private readonly AdvisoryDbContext _dbContext;

    public AdvisoryReportRepository(AdvisoryDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(AdvisoryReport report)
    {
        await _dbContext.AdvisoryReports.AddAsync(report);
        await _dbContext.SaveChangesWithOutboxAsync();
    }

    public async Task<AdvisoryReport?> GetLatestByFarmIdAsync(Guid farmId)
    {
        return await _dbContext.AdvisoryReports
            .Where(x => x.FarmId == farmId)
            .OrderByDescending(x => x.GeneratedOn)
            .FirstOrDefaultAsync();
    }
}
// using FarmManagement.Modules.Advisory.Application.Interfaces;
// using FarmManagement.Modules.Advisory.Domain.Aggregates;
// using FarmManagement.Modules.Advisory.Infrastructure.Persistence;
// using Microsoft.EntityFrameworkCore;

// namespace FarmManagement.Modules.Advisory.Infrastructure.Repositories;

// public class AdvisoryReportRepository : IAdvisoryReportRepository
// {
//     private readonly AdvisoryDbContext _dbContext;

//     public AdvisoryReportRepository(AdvisoryDbContext dbContext)
//     {
//         _dbContext = dbContext;
//     }

//     public async Task AddAsync(AdvisoryReport report)
//     {
//         await _dbContext.AdvisoryReports.AddAsync(report);
//         await _dbContext.SaveChangesAsync();
//     }

//     public async Task<AdvisoryReport?> GetLatestByFarmIdAsync(Guid farmId)
//     {
//         return await _dbContext.AdvisoryReports
//             .Where(x => x.FarmId == farmId)
//             .OrderByDescending(x => x.GeneratedOn)
//             .FirstOrDefaultAsync();
//     }
// }
