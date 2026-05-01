using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.PlaceTags;
using KHHub.MasterDataService.Services.Dtos.PlaceTags;

namespace KHHub.Web.Pages.PlaceTags;

public class IndexModel : IndexModelBase
{
    public IndexModel(IPlaceTagsAppService placeTagsAppService) : base(placeTagsAppService)
    {
    }
}