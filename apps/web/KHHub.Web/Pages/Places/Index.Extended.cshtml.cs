using KHHub.MasterDataService.Localization;
using Microsoft.Extensions.Localization;

namespace KHHub.Web.Pages.Places;

public class IndexModel : IndexModelBase
{
    public IndexModel(IStringLocalizer<MasterDataServiceResource> masterDataLocalizer)
        : base(masterDataLocalizer)
    {
    }
}
