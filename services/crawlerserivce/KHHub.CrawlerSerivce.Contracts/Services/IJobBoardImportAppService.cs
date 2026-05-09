using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Services.Dtos.Crawling;
using Volo.Abp.Application.Services;

namespace KHHub.CrawlerSerivce.Services;

/// <summary>
/// Listing → detail → normalize → MasterData <c>IJobCrawlerIntegrationService</c>.
/// </summary>
public interface IJobBoardImportAppService : IApplicationService
{
    Task<ImportJobListingResultDto> ImportFromListingAsync(ImportJobListingInput input);
}
