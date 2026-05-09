using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.JobViews;
using KHHub.MasterDataService.Services.Dtos.JobViews;

namespace KHHub.Web.Pages.JobViews;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public JobViewCreateViewModel JobView { get; set; }

    public List<SelectListItem> JobLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IJobViewsAppService _jobViewsAppService;

    public CreateModalModelBase(IJobViewsAppService jobViewsAppService)
    {
        _jobViewsAppService = jobViewsAppService;
        JobView = new();
    }

    public virtual async Task OnGetAsync()
    {
        JobView = new JobViewCreateViewModel();
        JobLookupListRequired.AddRange((await _jobViewsAppService.GetJobLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _jobViewsAppService.CreateAsync(ObjectMapper.Map<JobViewCreateViewModel, JobViewCreateDto>(JobView));
        return NoContent();
    }
}

public class JobViewCreateViewModel : JobViewCreateDto
{
}