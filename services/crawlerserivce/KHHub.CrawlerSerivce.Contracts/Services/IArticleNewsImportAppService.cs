using System.Threading.Tasks;
using KHHub.CrawlerSerivce.Services.Dtos.Crawling;
using Volo.Abp.Application.Services;

namespace KHHub.CrawlerSerivce.Services;

public interface IArticleNewsImportAppService : IApplicationService
{
    Task<ImportArticleListingResultDto> ImportFromListingAsync(ImportArticleListingInput input);
}
