using Microsoft.Extensions.DependencyInjection;

namespace FarmManagement.Modules.Farm.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddFarmApi(this IServiceCollection services)
    {
        services.AddControllers()
                .AddApplicationPart(typeof(DependencyInjection).Assembly);

        return services;
    }
}
