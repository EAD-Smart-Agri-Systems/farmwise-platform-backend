using Microsoft.Extensions.DependencyInjection;

namespace FarmManagement.WebApi.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationPolicies(
        this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("FarmerOnly", policy =>
                policy.RequireRole("farmer"));

            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("admin"));
        });

        return services;
    }
}
