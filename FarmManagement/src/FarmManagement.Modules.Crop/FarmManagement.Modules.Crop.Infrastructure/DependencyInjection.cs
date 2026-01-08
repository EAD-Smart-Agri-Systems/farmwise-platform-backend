using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FarmManagement.Modules.Crop.Application.Interfaces;
using FarmManagement.Modules.Crop.Infrastructure.Persistence;
using FarmManagement.Modules.Crop.Infrastructure.Persistence.Repositories;

namespace FarmManagement.Modules.Crop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCropInfrastructure(
        this IServiceCollection services)
    {
        services.AddDbContext<CropDbContext>(options =>
            options.UseInMemoryDatabase("CropDb"));

        services.AddScoped<ICropCycleRepository, CropCycleRepository>();

        return services;
    }
}
