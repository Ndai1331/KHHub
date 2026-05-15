using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Services;
using KHHub.CrawlerSerivce.Services.Dtos.Crawling;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace KHHub.CrawlerSerivce.BackgroundJobs;

public class ScheduledArticleNewsImportJob : AsyncBackgroundJob<ImportArticleListingInput>, ITransientDependency
{
    private readonly IArticleNewsImportAppService _articleNewsImportAppService;

    public ScheduledArticleNewsImportJob(IArticleNewsImportAppService articleNewsImportAppService)
    {
        _articleNewsImportAppService = articleNewsImportAppService;
    }

    public override async Task ExecuteAsync(ImportArticleListingInput args)
    {
        await _articleNewsImportAppService.ImportFromListingAsync(args);
    }
}
