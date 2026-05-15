using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Services.Dtos.Crawling;
using Volo.Abp.Application.Services;

namespace KHHub.CrawlerSerivce.Services;

public interface IArticleListingCrawlAppService : IApplicationService
{
    Task<PreviewArticleListingResultDto> PreviewListingAsync(PreviewArticleListingInput input);
}
