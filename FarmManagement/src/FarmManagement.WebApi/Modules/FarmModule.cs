namespace FarmManagement.WebApi.Modules;

public static class FarmModule
{
    public static IEndpointRouteBuilder MapFarmModule(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllers();
        return endpoints;
    }
}
