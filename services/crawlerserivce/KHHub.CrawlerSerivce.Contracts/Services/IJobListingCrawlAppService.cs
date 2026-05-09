using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Services.Dtos.Crawling;
using Volo.Abp.Application.Services;

namespace KHHub.CrawlerSerivce.Services;

/// <summary>
/// Preview crawling job listing pages (no MasterData write).
/// </summary>
public interface IJobListingCrawlAppService : IApplicationService
{
    Task<PreviewJobListingResultDto> PreviewListingAsync(PreviewJobListingInput input);
}
