namespace FarmManagement.WebApi.Modules;

public static class AdvisoryModule
{
    public static IEndpointRouteBuilder MapAdvisoryModule(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllers();
        return endpoints;
    }
}
