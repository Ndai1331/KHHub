using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Services;
using KHHub.CrawlerSerivce.Services.Dtos.Crawling;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace KHHub.CrawlerSerivce.BackgroundJobs;

public class ScheduledJobBoardImportJob : AsyncBackgroundJob<ImportJobListingInput>, ITransientDependency
{
    private readonly IJobBoardImportAppService _jobBoardImportAppService;

    public ScheduledJobBoardImportJob(IJobBoardImportAppService jobBoardImportAppService)
    {
        _jobBoardImportAppService = jobBoardImportAppService;
    }

    public override async Task ExecuteAsync(ImportJobListingInput args)
    {
        await _jobBoardImportAppService.ImportFromListingAsync(args);
    }
}
