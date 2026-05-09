using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.JobApplications;
using KHHub.MasterDataService.Services.Dtos.JobApplications;

namespace KHHub.Web.Pages.JobApplications;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public JobApplicationUpdateViewModel JobApplication { get; set; }

    public List<SelectListItem> JobLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IJobApplicationsAppService _jobApplicationsAppService;

    public EditModalModelBase(IJobApplicationsAppService jobApplicationsAppService)
    {
        _jobApplicationsAppService = jobApplicationsAppService;
        JobApplication = new();
    }

    public virtual async Task OnGetAsync()
    {
        var jobApplicationWithNavigationPropertiesDto = await _jobApplicationsAppService.GetWithNavigationPropertiesAsync(Id);
        JobApplication = ObjectMapper.Map<JobApplicationDto, JobApplicationUpdateViewModel>(jobApplicationWithNavigationPropertiesDto.JobApplication);
        JobLookupListRequired.AddRange((await _jobApplicationsAppService.GetJobLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _jobApplicationsAppService.UpdateAsync(Id, ObjectMapper.Map<JobApplicationUpdateViewModel, JobApplicationUpdateDto>(JobApplication));
        return NoContent();
    }
}

public class JobApplicationUpdateViewModel : JobApplicationUpdateDto
{
}