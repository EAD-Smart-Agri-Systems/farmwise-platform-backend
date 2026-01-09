using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FarmManagement.Modules.Farm.Infrastructure.Persistence;

public class FarmDbContextFactory : IDesignTimeDbContextFactory<FarmDbContext>
{
    public FarmDbContext CreateDbContext(string[] args)
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

        var optionsBuilder = new DbContextOptionsBuilder<FarmDbContext>();
        var connectionString = configuration.GetConnectionString("FarmDb") 
            ?? "Server=localhost;Database=FarmDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True";

        optionsBuilder.UseSqlServer(connectionString);

        return new FarmDbContext(optionsBuilder.Options);
    }
}
