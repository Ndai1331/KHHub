using KHHub.MasterDataService.Entities.Places;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.Places;

public abstract class IndexModelBase : AbpPageModel
{
    public Guid? PlaceCategoryIdFilter { get; set; }

    public Guid? ProvinceIdFilter { get; set; }

    public Guid? WardIdFilter { get; set; }

    public List<SelectListItem> PlaceStatusList { get; set; } = [];

    public List<SelectListItem> PriceRangeList { get; set; } = [];

    public PlaceStatus? StatusFilter { get; set; }

    public PriceRange? PriceRangeFilter { get; set; }

    public string IsFeaturedFilter { get; set; } = string.Empty;

    public List<SelectListItem> IsFeaturedFilterItems { get; set; } = [];

    public string IsHotFilter { get; set; } = string.Empty;

    public List<SelectListItem> IsHotFilterItems { get; set; } = [];

    public string IsVerifiedFilter { get; set; } = string.Empty;

    public List<SelectListItem> IsVerifiedFilterItems { get; set; } = [];

    protected IStringLocalizer<MasterDataServiceResource> _masterDataLocalizer;

    public IndexModelBase(IStringLocalizer<MasterDataServiceResource> masterDataLocalizer)
    {
        _masterDataLocalizer = masterDataLocalizer;
    }

    public virtual async Task OnGetAsync()
    {
        PlaceStatusList =
        [
            new SelectListItem(_masterDataLocalizer["All"].Value, string.Empty),
        ];
        PlaceStatusList.AddRange(
            Enum.GetValues<PlaceStatus>()
                .Select(s => new SelectListItem(_masterDataLocalizer[$"Enum:{nameof(PlaceStatus)}.{(int)s}"].Value, ((int)s).ToString())));

        PriceRangeList =
        [
            new SelectListItem(_masterDataLocalizer["All"].Value, string.Empty),
        ];
        PriceRangeList.AddRange(
            Enum.GetValues<PriceRange>()
                .Select(p => new SelectListItem(_masterDataLocalizer[$"Enum:{nameof(PriceRange)}.{(int)p}"].Value, ((int)p).ToString())));

        static List<SelectListItem> BoolFilterItems(IStringLocalizer<MasterDataServiceResource> loc) =>
        [
            new SelectListItem(loc["All"].Value, string.Empty),
            new SelectListItem(loc["Yes"].Value, "true"),
            new SelectListItem(loc["No"].Value, "false"),
        ];

        IsFeaturedFilterItems = BoolFilterItems(_masterDataLocalizer);
        IsHotFilterItems = BoolFilterItems(_masterDataLocalizer);
        IsVerifiedFilterItems = BoolFilterItems(_masterDataLocalizer);

        await Task.CompletedTask;
    }
}
