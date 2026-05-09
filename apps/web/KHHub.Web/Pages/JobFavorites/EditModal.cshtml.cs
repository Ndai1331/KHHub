using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.JobFavorites;
using KHHub.MasterDataService.Services.Dtos.JobFavorites;
using KHHub.MasterDataService.Services.Dtos.Jobs;

namespace KHHub.Web.Pages.JobFavorites;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public JobFavoriteUpdateViewModel JobFavorite { get; set; }

    public JobDto Job { get; set; }

    protected IJobFavoritesAppService _jobFavoritesAppService;

    public EditModalModelBase(IJobFavoritesAppService jobFavoritesAppService)
    {
        _jobFavoritesAppService = jobFavoritesAppService;
        JobFavorite = new();
    }

    public virtual async Task OnGetAsync()
    {
        var jobFavoriteWithNavigationPropertiesDto = await _jobFavoritesAppService.GetWithNavigationPropertiesAsync(Id);
        JobFavorite = ObjectMapper.Map<JobFavoriteDto, JobFavoriteUpdateViewModel>(jobFavoriteWithNavigationPropertiesDto.JobFavorite);
        Job = jobFavoriteWithNavigationPropertiesDto.Job;
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _jobFavoritesAppService.UpdateAsync(Id, ObjectMapper.Map<JobFavoriteUpdateViewModel, JobFavoriteUpdateDto>(JobFavorite));
        return NoContent();
    }
}

public class JobFavoriteUpdateViewModel : JobFavoriteUpdateDto
{
}