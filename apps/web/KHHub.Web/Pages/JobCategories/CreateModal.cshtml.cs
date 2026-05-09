using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.JobCategories;
using KHHub.MasterDataService.Services.Dtos.JobCategories;

namespace KHHub.Web.Pages.JobCategories;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public JobCategoryCreateViewModel JobCategory { get; set; }

    protected IJobCategoriesAppService _jobCategoriesAppService;

    public CreateModalModelBase(IJobCategoriesAppService jobCategoriesAppService)
    {
        _jobCategoriesAppService = jobCategoriesAppService;
        JobCategory = new();
    }

    public virtual async Task OnGetAsync()
    {
        JobCategory = new JobCategoryCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _jobCategoriesAppService.CreateAsync(ObjectMapper.Map<JobCategoryCreateViewModel, JobCategoryCreateDto>(JobCategory));
        return NoContent();
    }
}

public class JobCategoryCreateViewModel : JobCategoryCreateDto
{
}