using KHHub.MasterDataService.Entities.Places;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.Places;
using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.Places;

public abstract class IndexModelBase : AbpPageModel
{
    public string? NameFilter { get; set; }

    public string? SlugFilter { get; set; }

    public string? ShortDescriptionFilter { get; set; }

    public string? DescriptionFilter { get; set; }

    public string? ThumbnailUrlFilter { get; set; }

    public string? CoverImageUrlFilter { get; set; }

    public string? AddressFilter { get; set; }

    public decimal? LatitudeFilterMin { get; set; }

    public decimal? LatitudeFilterMax { get; set; }

    public decimal? LongitudedFilterMin { get; set; }

    public decimal? LongitudedFilterMax { get; set; }

    public string? PhoneNumberFilter { get; set; }

    public string? EmailFilter { get; set; }

    public string? WebsiteFilter { get; set; }

    public string? OpeningHoursFilter { get; set; }

    public PriceRange? PriceRangeFilter { get; set; }

    public string? GoogleMapUrlFilter { get; set; }

    public PlaceStatus? StatusFilter { get; set; }

    public int? ViewCountFilterMin { get; set; }

    public int? ViewCountFilterMax { get; set; }

    public int? FavoriteCountFilterMin { get; set; }

    public int? FavoriteCountFilterMax { get; set; }

    public int? ReviewCountFilterMin { get; set; }

    public int? ReviewCountFilterMax { get; set; }

    public decimal? RatingAveragedFilterMin { get; set; }

    public decimal? RatingAveragedFilterMax { get; set; }

    public int? RatingTotalFilterMin { get; set; }

    public int? RatingTotalFilterMax { get; set; }

    [SelectItems(nameof(IsFeaturedBoolFilterItems))]
    public string IsFeaturedFilter { get; set; }

    public List<SelectListItem> IsFeaturedBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    [SelectItems(nameof(IsHotBoolFilterItems))]
    public string IsHotFilter { get; set; }

    public List<SelectListItem> IsHotBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    [SelectItems(nameof(IsVerifiedBoolFilterItems))]
    public string IsVerifiedFilter { get; set; }

    public List<SelectListItem> IsVerifiedBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    public string? SeoTitleFilter { get; set; }

    public string? SeoDescriptionFilter { get; set; }

    public string? SeoKeywordsFilter { get; set; }

    [SelectItems(nameof(PlaceCategoryLookupList))]
    public Guid PlaceCategoryIdFilter { get; set; }

    public List<SelectListItem> PlaceCategoryLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };
    [SelectItems(nameof(ProvinceLookupList))]
    public Guid ProvinceIdFilter { get; set; }

    public List<SelectListItem> ProvinceLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };
    [SelectItems(nameof(WardLookupList))]
    public Guid WardIdFilter { get; set; }

    public List<SelectListItem> WardLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IPlacesAppService _placesAppService;

    public IndexModelBase(IPlacesAppService placesAppService)
    {
        _placesAppService = placesAppService;
    }

    public virtual async Task OnGetAsync()
    {
        PlaceCategoryLookupList.AddRange((await _placesAppService.GetPlaceCategoryLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        ProvinceLookupList.AddRange((await _placesAppService.GetProvinceLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        WardLookupList.AddRange((await _placesAppService.GetWardLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}