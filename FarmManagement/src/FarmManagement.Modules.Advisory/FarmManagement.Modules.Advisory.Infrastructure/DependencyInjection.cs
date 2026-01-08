using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FarmManagement.Modules.Advisory.Application.Interfaces;
using FarmManagement.Modules.Advisory.Infrastructure.Persistence;
using FarmManagement.Modules.Advisory.Infrastructure.Repositories;
using FarmManagement.Modules.Advisory.Infrastructure.Services;

namespace FarmManagement.Modules.Advisory.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddAdvisoryInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AdvisoryDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("AdvisoryDb")));

        services.AddScoped<IAdvisoryReportRepository, AdvisoryReportRepository>();
        services.AddScoped<IAdvisoryRiskAssessmentService, AdvisoryRiskAssessmentService>();

        return services;
    }
}
