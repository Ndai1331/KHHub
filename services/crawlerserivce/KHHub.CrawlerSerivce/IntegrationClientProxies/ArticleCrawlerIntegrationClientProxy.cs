// Manual client proxy (mirror JobCrawlerIntegrationClientProxy).
using System.Threading.Tasks;
using KHHub.MasterDataService.IntegrationServices;
using KHHub.MasterDataService.Services.Dtos.ArticleCrawler;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.ClientProxying;

namespace KHHub.MasterDataService.IntegrationServices;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IArticleCrawlerIntegrationService), typeof(ArticleCrawlerIntegrationClientProxy))]
[IntegrationService]
public partial class ArticleCrawlerIntegrationClientProxy : ClientProxyBase<IArticleCrawlerIntegrationService>, IArticleCrawlerIntegrationService
{
    public virtual async Task<ArticleCrawlerUpsertResultDto> UpsertFromCrawlerAsync(ArticleCrawlerUpsertInputDto input)
    {
        return await RequestAsync<ArticleCrawlerUpsertResultDto>(nameof(UpsertFromCrawlerAsync), new ClientProxyRequestTypeValue
        {
            { typeof(ArticleCrawlerUpsertInputDto), input }
        });
    }
}
