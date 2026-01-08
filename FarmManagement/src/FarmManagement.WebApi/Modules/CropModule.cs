namespace FarmManagement.WebApi.Modules;

public static class CropModule
{
    public static IEndpointRouteBuilder MapCropModule(
        this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapControllers();
        return endpoints;
    }
}
