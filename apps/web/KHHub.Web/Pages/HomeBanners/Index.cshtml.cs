using KHHub.MasterDataService.Entities.HomeBanners;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.HomeBanners;
using KHHub.MasterDataService.Services.Dtos.HomeBanners;

namespace KHHub.Web.Pages.HomeBanners;

public abstract class IndexModelBase : AbpPageModel
{
    public string? TitleFilter { get; set; }

    public string? SubtitleFilter { get; set; }

    public string? DescriptionFilter { get; set; }

    public string? ImageUrlFilter { get; set; }

    public string? MobileImageUrlFilter { get; set; }

    public string? ButtonTextFilter { get; set; }

    public string? ButtonUrlFilter { get; set; }

    public TargetType? TargetTypeFilter { get; set; }

    public string? TargetIdFilter { get; set; }

    public int? SortOrderFilterMin { get; set; }

    public int? SortOrderFilterMax { get; set; }

    [SelectItems(nameof(IsActiveBoolFilterItems))]
    public string IsActiveFilter { get; set; }

    public List<SelectListItem> IsActiveBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    public DateTime? StartDateFilterMin { get; set; }

    public DateTime? StartDateFilterMax { get; set; }

    public DateTime? EndDateFilterMin { get; set; }

    public DateTime? EndDateFilterMax { get; set; }

    protected IHomeBannersAppService _homeBannersAppService;

    public IndexModelBase(IHomeBannersAppService homeBannersAppService)
    {
        _homeBannersAppService = homeBannersAppService;
    }

    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}