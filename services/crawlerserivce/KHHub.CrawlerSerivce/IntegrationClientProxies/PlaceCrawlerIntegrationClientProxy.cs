using System.Threading.Tasks;
using KHHub.MasterDataService.IntegrationServices;
using KHHub.MasterDataService.Services.Dtos.PlaceCrawler;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.ClientProxying;

namespace KHHub.MasterDataService.IntegrationServices;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IPlaceCrawlerIntegrationService), typeof(PlaceCrawlerIntegrationClientProxy))]
[IntegrationService]
public partial class PlaceCrawlerIntegrationClientProxy : ClientProxyBase<IPlaceCrawlerIntegrationService>, IPlaceCrawlerIntegrationService
{
    public virtual async Task<PlaceCrawlerUpsertResultDto> UpsertFromCrawlerAsync(PlaceCrawlerUpsertInputDto input)
    {
        return await RequestAsync<PlaceCrawlerUpsertResultDto>(nameof(UpsertFromCrawlerAsync), new ClientProxyRequestTypeValue
        {
            { typeof(PlaceCrawlerUpsertInputDto), input }
        });
    }
}
