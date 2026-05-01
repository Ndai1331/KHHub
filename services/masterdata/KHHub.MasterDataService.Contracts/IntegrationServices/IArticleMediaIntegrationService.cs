using System.Threading.Tasks;
using KHHub.MasterDataService.Services.Dtos.ArticleMedia;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace KHHub.MasterDataService.IntegrationServices;

/// <summary>
/// Inter-service API for article/category image uploads stored in MinIO (via BLOB storing).
/// </summary>
[IntegrationService]
public interface IArticleMediaIntegrationService : IApplicationService
{
    Task<ArticleMediaUploadResultDto> UploadImageAsync(IRemoteStreamContent content);

    Task<IRemoteStreamContent> GetAsync(string name);
}
