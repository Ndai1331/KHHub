using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.JobTagMappings;
using KHHub.MasterDataService.Services.Dtos.JobTagMappings;

namespace KHHub.Web.Pages.JobTagMappings;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public JobTagMappingCreateViewModel JobTagMapping { get; set; }

    public List<SelectListItem> JobTagLookupListRequired { get; set; } = new List<SelectListItem> { };
    public List<SelectListItem> JobLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IJobTagMappingsAppService _jobTagMappingsAppService;

    public CreateModalModelBase(IJobTagMappingsAppService jobTagMappingsAppService)
    {
        _jobTagMappingsAppService = jobTagMappingsAppService;
        JobTagMapping = new();
    }

    public virtual async Task OnGetAsync()
    {
        JobTagMapping = new JobTagMappingCreateViewModel();
        JobTagLookupListRequired.AddRange((await _jobTagMappingsAppService.GetJobTagLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        JobLookupListRequired.AddRange((await _jobTagMappingsAppService.GetJobLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _jobTagMappingsAppService.CreateAsync(ObjectMapper.Map<JobTagMappingCreateViewModel, JobTagMappingCreateDto>(JobTagMapping));
        return NoContent();
    }
}

public class JobTagMappingCreateViewModel : JobTagMappingCreateDto
{
}