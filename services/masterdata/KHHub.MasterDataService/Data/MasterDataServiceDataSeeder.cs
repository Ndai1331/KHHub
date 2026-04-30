using Volo.Abp.DependencyInjection;

namespace KHHub.MasterDataService.Data;

public class MasterDataServiceDataSeeder : ITransientDependency
{
    private readonly ILogger<MasterDataServiceDataSeeder> _logger;

    public MasterDataServiceDataSeeder(
        ILogger<MasterDataServiceDataSeeder> logger)
    {
        _logger = logger;
    }

    public async Task SeedAsync(Guid? tenantId = null)
    {
        _logger.LogInformation("Seeding data...");
        
        //...
    }
}