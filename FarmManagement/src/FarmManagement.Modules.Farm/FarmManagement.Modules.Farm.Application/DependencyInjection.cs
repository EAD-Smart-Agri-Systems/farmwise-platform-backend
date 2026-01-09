using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FarmManagement.Modules.Farm.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddFarmApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}
