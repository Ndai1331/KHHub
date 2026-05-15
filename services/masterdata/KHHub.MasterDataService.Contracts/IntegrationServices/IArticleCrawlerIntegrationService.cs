using System.Threading.Tasks;
using KHHub.MasterDataService.Services.Dtos.ArticleCrawler;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace KHHub.MasterDataService.IntegrationServices;

/// <summary>
/// Upserts an article imported by crawler services (idempotent by SourceUrl).
/// </summary>
[IntegrationService]
public interface IArticleCrawlerIntegrationService : IApplicationService
{
    Task<ArticleCrawlerUpsertResultDto> UpsertFromCrawlerAsync(ArticleCrawlerUpsertInputDto input);
}
