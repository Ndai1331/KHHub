using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.JobViews;
using KHHub.MasterDataService.Services.Dtos.JobViews;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.JobViews;

public abstract class IndexModelBase : AbpPageModel
{
    public string? UserIdFilter { get; set; }

    public string? IpAddressFilter { get; set; }

    public string? DeviceFilter { get; set; }

    public DateTime? ViewedAtFilterMin { get; set; }

    public DateTime? ViewedAtFilterMax { get; set; }

    public int? DurationFilterMin { get; set; }

    public int? DurationFilterMax { get; set; }

    public string? SourceFilter { get; set; }

    [SelectItems(nameof(JobLookupList))]
    public Guid JobIdFilter { get; set; }

    public List<SelectListItem> JobLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IJobViewsAppService _jobViewsAppService;

    public IndexModelBase(IJobViewsAppService jobViewsAppService)
    {
        _jobViewsAppService = jobViewsAppService;
    }

    public virtual async Task OnGetAsync()
    {
        JobLookupList.AddRange((await _jobViewsAppService.GetJobLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}