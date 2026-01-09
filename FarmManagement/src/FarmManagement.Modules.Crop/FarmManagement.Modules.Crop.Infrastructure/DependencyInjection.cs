using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FarmManagement.Modules.Crop.Application.Interfaces;
using FarmManagement.Modules.Crop.Infrastructure.Persistence;
using FarmManagement.Modules.Crop.Infrastructure.Persistence.Repositories;
using FarmManagement.Modules.Crop.Infrastructure.EventHandlers;

namespace FarmManagement.Modules.Crop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCropInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CropDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("CropDb")));

        services.AddScoped<ICropCycleRepository, CropCycleRepository>();

        // Register event handlers
        services.AddScoped<FarmRegisteredEventHandler>();
        services.AddScoped<FieldAddedToFarmEventHandler>();

        // Register background services for event subscription
        services.AddHostedService<FarmRegisteredSubscriberService>();
        services.AddHostedService<FieldAddedToFarmSubscriberService>();

        return services;
    }
}
