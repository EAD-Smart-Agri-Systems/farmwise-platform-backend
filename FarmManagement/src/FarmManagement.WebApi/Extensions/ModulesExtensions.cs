using FarmManagement.Infrastructure.Shared.DependencyInjection;
using FarmManagement.Modules.Advisory.Infrastructure;
using FarmManagement.Modules.Crop.Application;
using FarmManagement.Modules.Crop.Infrastructure;
using FarmManagement.Modules.Farm.Application;
using FarmManagement.Modules.Farm.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FarmManagement.WebApi.Extensions;

public static class ModulesExtensions
{
    public static IServiceCollection AddModules(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register Infrastructure.Shared (RabbitMQ, Outbox, Quartz)
        services.AddInfrastructureShared(configuration);

        // Register Application layers
        services.AddCropApplication();
        services.AddFarmApplication();

        // Register Infrastructure layers
        services.AddAdvisoryInfrastructure(configuration);

        services.AddCropInfrastructure(configuration);
        services.AddFarmInfrastructure(configuration);

        return services;
    }
}