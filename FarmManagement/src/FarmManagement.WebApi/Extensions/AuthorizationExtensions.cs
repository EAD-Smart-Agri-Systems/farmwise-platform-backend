using Microsoft.Extensions.DependencyInjection;

namespace FarmManagement.WebApi.Extensions;

public static class AuthorizationExtensions
{
    public static IServiceCollection AddAuthorizationPolicies(
        this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // Farmer policy - standard users
            options.AddPolicy("FarmerOnly", policy =>
                policy.RequireRole("farmer"));

            // Admin policy - administrators
            options.AddPolicy("AdminOnly", policy =>
                policy.RequireRole("admin"));

            // Combined policy for endpoints that require either role
            options.AddPolicy("FarmerOrAdmin", policy =>
                policy.RequireRole("farmer", "admin"));
        });

        return services;
    }
}
