using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.Jobs;
using KHHub.MasterDataService.Services.Dtos.Jobs;

namespace KHHub.Web.Pages.Jobs;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public JobCreateViewModel Job { get; set; }

    public List<SelectListItem> ProvinceLookupListRequired { get; set; } = new List<SelectListItem> { };
    public List<SelectListItem> WardLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IJobsAppService _jobsAppService;

    public CreateModalModelBase(IJobsAppService jobsAppService)
    {
        _jobsAppService = jobsAppService;
        Job = new();
    }

    public virtual async Task OnGetAsync()
    {
        Job = new JobCreateViewModel();
        ProvinceLookupListRequired.AddRange((await _jobsAppService.GetProvinceLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        WardLookupListRequired.AddRange((await _jobsAppService.GetWardLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _jobsAppService.CreateAsync(ObjectMapper.Map<JobCreateViewModel, JobCreateDto>(Job));
        return NoContent();
    }
}

public class JobCreateViewModel : JobCreateDto
{
}