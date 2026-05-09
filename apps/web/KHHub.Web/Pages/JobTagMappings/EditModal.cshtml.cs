using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.JobTagMappings;
using KHHub.MasterDataService.Services.Dtos.JobTagMappings;

namespace KHHub.Web.Pages.JobTagMappings;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public JobTagMappingUpdateViewModel JobTagMapping { get; set; }

    public List<SelectListItem> JobTagLookupListRequired { get; set; } = new List<SelectListItem> { };
    public List<SelectListItem> JobLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IJobTagMappingsAppService _jobTagMappingsAppService;

    public EditModalModelBase(IJobTagMappingsAppService jobTagMappingsAppService)
    {
        _jobTagMappingsAppService = jobTagMappingsAppService;
        JobTagMapping = new();
    }

    public virtual async Task OnGetAsync()
    {
        var jobTagMappingWithNavigationPropertiesDto = await _jobTagMappingsAppService.GetWithNavigationPropertiesAsync(Id);
        JobTagMapping = ObjectMapper.Map<JobTagMappingDto, JobTagMappingUpdateViewModel>(jobTagMappingWithNavigationPropertiesDto.JobTagMapping);
        JobTagLookupListRequired.AddRange((await _jobTagMappingsAppService.GetJobTagLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        JobLookupListRequired.AddRange((await _jobTagMappingsAppService.GetJobLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _jobTagMappingsAppService.UpdateAsync(Id, ObjectMapper.Map<JobTagMappingUpdateViewModel, JobTagMappingUpdateDto>(JobTagMapping));
        return NoContent();
    }
}

public class JobTagMappingUpdateViewModel : JobTagMappingUpdateDto
{
}