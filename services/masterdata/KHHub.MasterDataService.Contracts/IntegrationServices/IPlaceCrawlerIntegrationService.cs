using System.Threading.Tasks;
using KHHub.MasterDataService.Services.Dtos.PlaceCrawler;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace KHHub.MasterDataService.IntegrationServices;

/// <summary>
/// Upserts a place imported by crawler services (idempotent by SourceUrl).
/// </summary>
[IntegrationService]
public interface IPlaceCrawlerIntegrationService : IApplicationService
{
    Task<PlaceCrawlerUpsertResultDto> UpsertFromCrawlerAsync(PlaceCrawlerUpsertInputDto input);
}
