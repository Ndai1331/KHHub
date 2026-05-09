using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.JobViews;
using KHHub.MasterDataService.Services.Dtos.JobViews;

namespace KHHub.Web.Pages.JobViews;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public JobViewUpdateViewModel JobView { get; set; }

    public List<SelectListItem> JobLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IJobViewsAppService _jobViewsAppService;

    public EditModalModelBase(IJobViewsAppService jobViewsAppService)
    {
        _jobViewsAppService = jobViewsAppService;
        JobView = new();
    }

    public virtual async Task OnGetAsync()
    {
        var jobViewWithNavigationPropertiesDto = await _jobViewsAppService.GetWithNavigationPropertiesAsync(Id);
        JobView = ObjectMapper.Map<JobViewDto, JobViewUpdateViewModel>(jobViewWithNavigationPropertiesDto.JobView);
        JobLookupListRequired.AddRange((await _jobViewsAppService.GetJobLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _jobViewsAppService.UpdateAsync(Id, ObjectMapper.Map<JobViewUpdateViewModel, JobViewUpdateDto>(JobView));
        return NoContent();
    }
}

public class JobViewUpdateViewModel : JobViewUpdateDto
{
}