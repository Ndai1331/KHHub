using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.JobFavorites;
using KHHub.MasterDataService.Services.Dtos.JobFavorites;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.JobFavorites;

public abstract class IndexModelBase : AbpPageModel
{
    public string? UserIdFilter { get; set; }

    [SelectItems(nameof(JobLookupList))]
    public Guid JobIdFilter { get; set; }

    public List<SelectListItem> JobLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IJobFavoritesAppService _jobFavoritesAppService;

    public IndexModelBase(IJobFavoritesAppService jobFavoritesAppService)
    {
        _jobFavoritesAppService = jobFavoritesAppService;
    }

    public virtual async Task OnGetAsync()
    {
        JobLookupList.AddRange((await _jobFavoritesAppService.GetJobLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}