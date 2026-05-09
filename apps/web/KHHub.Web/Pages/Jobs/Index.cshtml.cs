using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.Jobs;
using KHHub.MasterDataService.Services.Dtos.Jobs;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.Jobs;

public abstract class IndexModelBase : AbpPageModel
{
    public string? TitleFilter { get; set; }

    public string? SlugFilter { get; set; }

    public string? SummaryFilter { get; set; }

    public string? DescriptionFilter { get; set; }

    public string? RequirementsFilter { get; set; }

    public string? BenefitsFilter { get; set; }

    public string? ThumbnailUrlFilter { get; set; }

    public string? CoverImageUrlFilter { get; set; }

    public EmploymentType? EmploymentTypeFilter { get; set; }

    public WorkMode? WorkModeFilter { get; set; }

    public ExperienceLevel? ExperienceLevelFilter { get; set; }

    public decimal? SalaryMinFilterMin { get; set; }

    public decimal? SalaryMinFilterMax { get; set; }

    public decimal? SalaryMaxFilterMin { get; set; }

    public decimal? SalaryMaxFilterMax { get; set; }

    public string? SalaryTextFilter { get; set; }

    public string? SalaryCurrencyFilter { get; set; }

    public string? LocationFilter { get; set; }

    public string? ContactEmailFilter { get; set; }

    public string? ContactPhoneFilter { get; set; }

    public string? ApplicationUrlFilter { get; set; }

    public DateTime? PublishedAtFilterMin { get; set; }

    public DateTime? PublishedAtFilterMax { get; set; }

    public JobStatus? StatusFilter { get; set; }

    public int? ViewCountFilterMin { get; set; }

    public int? ViewCountFilterMax { get; set; }

    public int? ApplicationCountFilterMin { get; set; }

    public int? ApplicationCountFilterMax { get; set; }

    public int? FavoriteCountFilterMin { get; set; }

    public int? FavoriteCountFilterMax { get; set; }

    public int? ShareCountFilterMin { get; set; }

    public int? ShareCountFilterMax { get; set; }

    [SelectItems(nameof(IsFeaturedBoolFilterItems))]
    public string IsFeaturedFilter { get; set; }

    public List<SelectListItem> IsFeaturedBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    [SelectItems(nameof(IsUrgentBoolFilterItems))]
    public string IsUrgentFilter { get; set; }

    public List<SelectListItem> IsUrgentBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    [SelectItems(nameof(IsHotBoolFilterItems))]
    public string IsHotFilter { get; set; }

    public List<SelectListItem> IsHotBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    public string? SeoTitleFilter { get; set; }

    public string? SeoDescriptionFilter { get; set; }

    public string? SeoKeywordsFilter { get; set; }

    [SelectItems(nameof(ProvinceLookupList))]
    public Guid ProvinceIdFilter { get; set; }

    public List<SelectListItem> ProvinceLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };
    [SelectItems(nameof(WardLookupList))]
    public Guid WardIdFilter { get; set; }

    public List<SelectListItem> WardLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };
    [SelectItems(nameof(JobCategoryLookupList))]
    public Guid JobCategoryIdFilter { get; set; }

    public List<SelectListItem> JobCategoryLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IJobsAppService _jobsAppService;

    public IndexModelBase(IJobsAppService jobsAppService)
    {
        _jobsAppService = jobsAppService;
    }

    public virtual async Task OnGetAsync()
    {
        ProvinceLookupList.AddRange((await _jobsAppService.GetProvinceLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        WardLookupList.AddRange((await _jobsAppService.GetWardLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        JobCategoryLookupList.AddRange((await _jobsAppService.GetJobCategoryLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}