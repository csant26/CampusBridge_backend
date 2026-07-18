using backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace backend;

public class CampusBridgeDbContextFactory : IDesignTimeDbContextFactory<CampusBridgeDbContext>
{
    public CampusBridgeDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<CampusBridgeDbContext>();
        optionsBuilder
            .UseNpgsql(DatabaseConfig.GetConnectionString(configuration))
            .UseSnakeCaseNamingConvention();

        return new CampusBridgeDbContext(optionsBuilder.Options);
    }
}
