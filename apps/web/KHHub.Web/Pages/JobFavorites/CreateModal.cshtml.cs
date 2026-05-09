using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.JobFavorites;
using KHHub.MasterDataService.Services.Dtos.JobFavorites;

namespace KHHub.Web.Pages.JobFavorites;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public JobFavoriteCreateViewModel JobFavorite { get; set; }

    protected IJobFavoritesAppService _jobFavoritesAppService;

    public CreateModalModelBase(IJobFavoritesAppService jobFavoritesAppService)
    {
        _jobFavoritesAppService = jobFavoritesAppService;
        JobFavorite = new();
    }

    public virtual async Task OnGetAsync()
    {
        JobFavorite = new JobFavoriteCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _jobFavoritesAppService.CreateAsync(ObjectMapper.Map<JobFavoriteCreateViewModel, JobFavoriteCreateDto>(JobFavorite));
        return NoContent();
    }
}

public class JobFavoriteCreateViewModel : JobFavoriteCreateDto
{
}