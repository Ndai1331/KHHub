using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.JobTags;
using KHHub.MasterDataService.Services.Dtos.JobTags;

namespace KHHub.Web.Pages.JobTags;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public JobTagCreateViewModel JobTag { get; set; }

    protected IJobTagsAppService _jobTagsAppService;

    public CreateModalModelBase(IJobTagsAppService jobTagsAppService)
    {
        _jobTagsAppService = jobTagsAppService;
        JobTag = new();
    }

    public virtual async Task OnGetAsync()
    {
        JobTag = new JobTagCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _jobTagsAppService.CreateAsync(ObjectMapper.Map<JobTagCreateViewModel, JobTagCreateDto>(JobTag));
        return NoContent();
    }
}

public class JobTagCreateViewModel : JobTagCreateDto
{
}