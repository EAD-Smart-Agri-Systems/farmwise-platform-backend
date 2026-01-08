using FarmManagement.Modules.Advisory.Infrastructure;
using FarmManagement.Modules.Crop.Infrastructure;
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
        services.AddAdvisoryInfrastructure(
            configuration.GetConnectionString("AdvisoryDb")!);

        services.AddCropInfrastructure(configuration);
        services.AddFarmInfrastructure(configuration);

        return services;
    }
}