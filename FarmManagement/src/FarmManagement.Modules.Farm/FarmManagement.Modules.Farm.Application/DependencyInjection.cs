using Microsoft.Extensions.DependencyInjection;

namespace FarmManagement.Modules.Farm.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddFarmApplication(this IServiceCollection services)
    {
        services.AddScoped<Commands.RegisterFarm.RegisterFarmCommandHandler>();
        services.AddScoped<Commands.AddFieldToFarm.AddFieldToFarmCommandHandler>();

        return services;
    }
}
