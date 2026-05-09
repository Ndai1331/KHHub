using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.JobTagMappings;
using KHHub.MasterDataService.Services.Dtos.JobTagMappings;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.JobTagMappings;

public abstract class IndexModelBase : AbpPageModel
{
    [SelectItems(nameof(IsPrimaryBoolFilterItems))]
    public string IsPrimaryFilter { get; set; }

    public List<SelectListItem> IsPrimaryBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    public string? SortOrderFilter { get; set; }

    [SelectItems(nameof(JobTagLookupList))]
    public Guid JobTagIdFilter { get; set; }

    public List<SelectListItem> JobTagLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };
    [SelectItems(nameof(JobLookupList))]
    public Guid JobIdFilter { get; set; }

    public List<SelectListItem> JobLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IJobTagMappingsAppService _jobTagMappingsAppService;

    public IndexModelBase(IJobTagMappingsAppService jobTagMappingsAppService)
    {
        _jobTagMappingsAppService = jobTagMappingsAppService;
    }

    public virtual async Task OnGetAsync()
    {
        JobTagLookupList.AddRange((await _jobTagMappingsAppService.GetJobTagLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        JobLookupList.AddRange((await _jobTagMappingsAppService.GetJobLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}