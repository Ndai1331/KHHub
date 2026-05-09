using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.JobTags;
using KHHub.MasterDataService.Services.Dtos.JobTags;

namespace KHHub.Web.Pages.JobTags;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public JobTagUpdateViewModel JobTag { get; set; }

    protected IJobTagsAppService _jobTagsAppService;

    public EditModalModelBase(IJobTagsAppService jobTagsAppService)
    {
        _jobTagsAppService = jobTagsAppService;
        JobTag = new();
    }

    public virtual async Task OnGetAsync()
    {
        var jobTag = await _jobTagsAppService.GetAsync(Id);
        JobTag = ObjectMapper.Map<JobTagDto, JobTagUpdateViewModel>(jobTag);
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _jobTagsAppService.UpdateAsync(Id, ObjectMapper.Map<JobTagUpdateViewModel, JobTagUpdateDto>(JobTag));
        return NoContent();
    }
}

public class JobTagUpdateViewModel : JobTagUpdateDto
{
}