// Manual client proxy (mirror Web ClientProxies pattern). Regenerate with abp generate-proxy if preferred.
using System.Threading.Tasks;
using KHHub.MasterDataService.IntegrationServices;
using KHHub.MasterDataService.Services.Dtos.JobCrawler;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Http.Client.ClientProxying;

// ReSharper disable once CheckNamespace
namespace KHHub.MasterDataService.IntegrationServices;

[Dependency(ReplaceServices = true)]
[ExposeServices(typeof(IJobCrawlerIntegrationService), typeof(JobCrawlerIntegrationClientProxy))]
[IntegrationService]
public partial class JobCrawlerIntegrationClientProxy : ClientProxyBase<IJobCrawlerIntegrationService>, IJobCrawlerIntegrationService
{
    public virtual async Task<JobCrawlerUpsertResultDto> UpsertFromCrawlerAsync(JobCrawlerUpsertInputDto input)
    {
        return await RequestAsync<JobCrawlerUpsertResultDto>(nameof(UpsertFromCrawlerAsync), new ClientProxyRequestTypeValue
        {
            { typeof(JobCrawlerUpsertInputDto), input }
        });
    }
}
