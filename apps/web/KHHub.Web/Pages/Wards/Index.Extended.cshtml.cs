using System;
using KHHub.MasterDataService.Services.Wards;

namespace KHHub.Web.Pages.Wards;

public class IndexModel : IndexModelBase
{
    public IndexModel(IWardsAppService wardsAppService) : base(wardsAppService)
    {
    }
}
