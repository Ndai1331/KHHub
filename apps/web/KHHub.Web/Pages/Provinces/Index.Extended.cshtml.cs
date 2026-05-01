using KHHub.MasterDataService.Services.Provinces;

namespace KHHub.Web.Pages.Provinces;

public class IndexModel : IndexModelBase
{
    public IndexModel(IProvincesAppService provincesAppService) : base(provincesAppService)
    {
    }
}
