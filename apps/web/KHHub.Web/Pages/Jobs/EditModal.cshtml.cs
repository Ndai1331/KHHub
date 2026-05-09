using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.JobCategories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.Jobs;
using KHHub.MasterDataService.Services.Dtos.JobCategories;
using KHHub.MasterDataService.Services.Dtos.Jobs;

namespace KHHub.Web.Pages.Jobs;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public JobUpdateViewModel Job { get; set; }

    public JobCategoryDto JobCategory { get; set; }

    public List<SelectListItem> ProvinceLookupListRequired { get; set; } = new List<SelectListItem> { };
    public List<SelectListItem> WardLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IJobsAppService _jobsAppService;

    public EditModalModelBase(IJobsAppService jobsAppService)
    {
        _jobsAppService = jobsAppService;
        Job = new();
    }

    public virtual async Task OnGetAsync()
    {
        var jobWithNavigationPropertiesDto = await _jobsAppService.GetWithNavigationPropertiesAsync(Id);
        Job = ObjectMapper.Map<JobDto, JobUpdateViewModel>(jobWithNavigationPropertiesDto.Job);
        JobCategory = jobWithNavigationPropertiesDto.JobCategory;
        ProvinceLookupListRequired.AddRange((await _jobsAppService.GetProvinceLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        WardLookupListRequired.AddRange((await _jobsAppService.GetWardLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _jobsAppService.UpdateAsync(Id, ObjectMapper.Map<JobUpdateViewModel, JobUpdateDto>(Job));
        return NoContent();
    }
}

public class JobUpdateViewModel : JobUpdateDto
{
}