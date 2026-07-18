using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace backend.Data;

public class CampusBridgeAuthDbContextFactory : IDesignTimeDbContextFactory<CampusBridgeAuthDbContext>
{
    public CampusBridgeAuthDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile("appsettings.Development.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<CampusBridgeAuthDbContext>();
        optionsBuilder
            .UseNpgsql(DatabaseConfig.GetConnectionString(configuration))
            .UseSnakeCaseNamingConvention();

        return new CampusBridgeAuthDbContext(optionsBuilder.Options);
    }
}
