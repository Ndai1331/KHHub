using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.Provinces;
using KHHub.MasterDataService.Services.Dtos.Provinces;

namespace KHHub.Web.Pages.Provinces;

public abstract class IndexModelBase : AbpPageModel
{
    public string? CodeFilter { get; set; }

    public string? NameFilter { get; set; }

    protected IProvincesAppService _provincesAppService;

    public IndexModelBase(IProvincesAppService provincesAppService)
    {
        _provincesAppService = provincesAppService;
    }

    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}