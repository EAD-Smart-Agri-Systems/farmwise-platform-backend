using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FarmManagement.Modules.Crop.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddCropApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}