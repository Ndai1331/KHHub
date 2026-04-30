using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.Wards;
using KHHub.MasterDataService.Services.Dtos.Wards;

namespace KHHub.Web.Pages.Wards;

public class IndexModel : IndexModelBase
{
    public IndexModel(IWardsAppService wardsAppService) : base(wardsAppService)
    {
    }
}