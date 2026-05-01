using KHHub.MasterDataService.Localization;
using Microsoft.Extensions.Localization;

namespace KHHub.Web.Pages.ArticleCategories;

public class IndexModel : IndexModelBase
{
    public IndexModel(IStringLocalizer<MasterDataServiceResource> masterDataLocalizer)
        : base(masterDataLocalizer)
    {
    }
}
