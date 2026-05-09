using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.JobCategories;
using KHHub.MasterDataService.Services.Dtos.JobCategories;

namespace KHHub.Web.Pages.JobCategories;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public JobCategoryUpdateViewModel JobCategory { get; set; }

    protected IJobCategoriesAppService _jobCategoriesAppService;

    public EditModalModelBase(IJobCategoriesAppService jobCategoriesAppService)
    {
        _jobCategoriesAppService = jobCategoriesAppService;
        JobCategory = new();
    }

    public virtual async Task OnGetAsync()
    {
        var jobCategory = await _jobCategoriesAppService.GetAsync(Id);
        JobCategory = ObjectMapper.Map<JobCategoryDto, JobCategoryUpdateViewModel>(jobCategory);
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _jobCategoriesAppService.UpdateAsync(Id, ObjectMapper.Map<JobCategoryUpdateViewModel, JobCategoryUpdateDto>(JobCategory));
        return NoContent();
    }
}

public class JobCategoryUpdateViewModel : JobCategoryUpdateDto
{
}