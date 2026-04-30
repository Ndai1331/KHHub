using KHHub.MasterDataService.Localization;
using Volo.Abp.Application.Services;

namespace KHHub.MasterDataService;

public abstract class MasterDataAppService : ApplicationService
{
    protected MasterDataAppService()
    {
        LocalizationResource = typeof(MasterDataServiceResource);
        ObjectMapperContext = typeof(KHHubMasterDataServiceModule);
    }
}