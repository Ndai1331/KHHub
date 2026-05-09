using System.Threading.Tasks;
using KHHub.MasterDataService.Services.Dtos.JobCrawler;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace KHHub.MasterDataService.IntegrationServices;

/// <summary>
/// Upserts a job imported by crawler services (idempotent by ApplicationUrl).
/// </summary>
[IntegrationService]
public interface IJobCrawlerIntegrationService : IApplicationService
{
    Task<JobCrawlerUpsertResultDto> UpsertFromCrawlerAsync(JobCrawlerUpsertInputDto input);
}
