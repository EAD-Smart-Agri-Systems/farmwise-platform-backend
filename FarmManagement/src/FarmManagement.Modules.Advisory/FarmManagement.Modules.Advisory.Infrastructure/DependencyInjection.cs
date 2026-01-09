using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FarmManagement.Modules.Advisory.Application.Interfaces;
using FarmManagement.Modules.Advisory.Application.Commands.GenerateAdvisoryReport;
using FarmManagement.Modules.Advisory.Infrastructure.Persistence;
using FarmManagement.Modules.Advisory.Infrastructure.Repositories;
using FarmManagement.Modules.Advisory.Infrastructure.Services;
using FarmManagement.Modules.Advisory.Infrastructure.EventHandlers;

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
        services.AddScoped<GenerateAdvisoryReportHandler>();
        services.AddScoped<CropCycleStartedEventHandler>();
        services.AddScoped<GrowthStageAdvancedEventHandler>();

        // Register background services for event subscription
        services.AddHostedService<AdvisoryEventSubscriberService>();
        services.AddHostedService<GrowthStageAdvancedSubscriberService>();

        return services;
    }
}