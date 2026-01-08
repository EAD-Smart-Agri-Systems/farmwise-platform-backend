using FarmManagement.Modules.Farm.Domain.Repositories;
using FarmManagement.Modules.Farm.Infrastructure.Persistence;
using FarmManagement.Modules.Farm.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FarmManagement.Modules.Farm.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddFarmInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<FarmDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("FarmDb")));

        services.AddScoped<IFarmRepository, FarmRepository>();

        return services;
    }
}
