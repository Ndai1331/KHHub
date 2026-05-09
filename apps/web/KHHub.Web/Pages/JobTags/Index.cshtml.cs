using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.JobTags;
using KHHub.MasterDataService.Services.Dtos.JobTags;

namespace KHHub.Web.Pages.JobTags;

public abstract class IndexModelBase : AbpPageModel
{
    public string? NameFilter { get; set; }

    public string? SlugFilter { get; set; }

    public string? DescriptionFilter { get; set; }

    public int? UsageCountFilterMin { get; set; }

    public int? UsageCountFilterMax { get; set; }

    protected IJobTagsAppService _jobTagsAppService;

    public IndexModelBase(IJobTagsAppService jobTagsAppService)
    {
        _jobTagsAppService = jobTagsAppService;
    }

    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}