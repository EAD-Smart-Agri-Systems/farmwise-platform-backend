using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FarmManagement.Modules.Crop.Infrastructure.Persistence;

public class CropDbContextFactory : IDesignTimeDbContextFactory<CropDbContext>
{
    public CropDbContext CreateDbContext(string[] args)
    {
        // Try to find appsettings.json in the WebApi project (startup project)
        var basePath = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", "..", "FarmManagement.WebApi");
        if (!Directory.Exists(basePath))
        {
            basePath = Directory.GetCurrentDirectory();
        }

        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", optional: true)
            .AddJsonFile("appsettings.Development.json", optional: true)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<CropDbContext>();
        var connectionString = configuration.GetConnectionString("CropDb") 
            ?? "Server=localhost;Database=CropDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True";

        optionsBuilder.UseSqlServer(connectionString);

        return new CropDbContext(optionsBuilder.Options);
    }
}
