using KHHub.MasterDataService.Entities.HomeBanners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.HomeBanners;
using KHHub.MasterDataService.Services.Dtos.HomeBanners;

namespace KHHub.Web.Pages.HomeBanners;

public class IndexModel : IndexModelBase
{
    public IndexModel(IHomeBannersAppService homeBannersAppService) : base(homeBannersAppService)
    {
    }
}