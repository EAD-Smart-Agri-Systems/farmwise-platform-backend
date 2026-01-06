using FarmManagement.Modules.Advisory.Application.Interfaces;
using FarmManagement.Modules.Advisory.Infrastructure.Persistence;
using FarmManagement.Modules.Advisory.Infrastructure.Repositories;
using FarmManagement.Modules.Advisory.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FarmManagement.Modules.Advisory.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAdvisoryInfrastructure(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<AdvisoryDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IAdvisoryReportRepository, AdvisoryReportRepository>();
        services.AddScoped<IAdvisoryRiskAssessmentService, AdvisoryRiskAssessmentService>();

        return services;
    }
}
