using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.PlaceTagMappings;
using KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;

namespace KHHub.Web.Pages.PlaceTagMappings;

public class IndexModel : IndexModelBase
{
    public IndexModel(IPlaceTagMappingsAppService placeTagMappingsAppService) : base(placeTagMappingsAppService)
    {
    }
}