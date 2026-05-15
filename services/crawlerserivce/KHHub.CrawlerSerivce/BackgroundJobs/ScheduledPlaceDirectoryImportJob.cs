using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Services;
using KHHub.CrawlerSerivce.Services.Dtos.Crawling;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace KHHub.CrawlerSerivce.BackgroundJobs;

public class ScheduledPlaceDirectoryImportJob : AsyncBackgroundJob<ImportPlaceListingInput>, ITransientDependency
{
    private readonly IPlaceDirectoryImportAppService _placeDirectoryImportAppService;

    public ScheduledPlaceDirectoryImportJob(IPlaceDirectoryImportAppService placeDirectoryImportAppService)
    {
        _placeDirectoryImportAppService = placeDirectoryImportAppService;
    }

    public override async Task ExecuteAsync(ImportPlaceListingInput args)
    {
        await _placeDirectoryImportAppService.ImportFromListingAsync(args);
    }
}
