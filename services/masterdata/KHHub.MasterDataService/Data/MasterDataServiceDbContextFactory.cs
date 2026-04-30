using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KHHub.MasterDataService.Data;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands)
 * */
public class MasterDataServiceDbContextFactory : IDesignTimeDbContextFactory<MasterDataServiceDbContext>
{
    [ModuleInitializer]
    public static void Initialize()
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }
    
    public MasterDataServiceDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<MasterDataServiceDbContext>()
            .UseNpgsql(GetConnectionStringFromConfiguration(), b =>
            {
                b.MigrationsHistoryTable("__MasterDataService_Migrations");
            });

        return new MasterDataServiceDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration().GetConnectionString(MasterDataServiceDbContext.DatabaseName)
               ?? throw new ApplicationException($"Could not find a connection string named '{MasterDataServiceDbContext.DatabaseName}'.");
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false)
            .AddEnvironmentVariables();

        return builder.Build();
    }
}
