using Volo.Abp.DependencyInjection;

namespace KHHub.CrawlerSerivce.Data;

public class CrawlerSerivceDataSeeder : ITransientDependency
{
    private readonly ILogger<CrawlerSerivceDataSeeder> _logger;

    public CrawlerSerivceDataSeeder(
        ILogger<CrawlerSerivceDataSeeder> logger)
    {
        _logger = logger;
    }

    public async Task SeedAsync(Guid? tenantId = null)
    {
        _logger.LogInformation("Seeding data...");
        
        //...
    }
}