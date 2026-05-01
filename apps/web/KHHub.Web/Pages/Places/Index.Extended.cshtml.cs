using KHHub.MasterDataService.Entities.Places;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.Places;
using KHHub.MasterDataService.Services.Dtos.Places;

namespace KHHub.Web.Pages.Places;

public class IndexModel : IndexModelBase
{
    public IndexModel(IPlacesAppService placesAppService) : base(placesAppService)
    {
    }
}