using KHHub.CrawlerSerivce.Localization;
using Volo.Abp.Application.Services;

namespace KHHub.CrawlerSerivce;

public abstract class CrawlerSerivceAppService : ApplicationService
{
    protected CrawlerSerivceAppService()
    {
        LocalizationResource = typeof(CrawlerSerivceResource);
        ObjectMapperContext = typeof(KHHubCrawlerSerivceModule);
    }
}