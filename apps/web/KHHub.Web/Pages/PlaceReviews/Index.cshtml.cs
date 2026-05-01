using KHHub.MasterDataService.Entities.PlaceReviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.PlaceReviews;
using KHHub.MasterDataService.Services.Dtos.PlaceReviews;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.PlaceReviews;

public abstract class IndexModelBase : AbpPageModel
{
    public int? RatingFilterMin { get; set; }

    public int? RatingFilterMax { get; set; }

    public string? TitleFilter { get; set; }

    public string? CommentFilter { get; set; }

    public int? LikeCountFilterMin { get; set; }

    public int? LikeCountFilterMax { get; set; }

    public PlaceReviewStatus? StatusFilter { get; set; }

    public string? UserIdFilter { get; set; }

    [SelectItems(nameof(PlaceLookupList))]
    public Guid PlaceIdFilter { get; set; }

    public List<SelectListItem> PlaceLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IPlaceReviewsAppService _placeReviewsAppService;

    public IndexModelBase(IPlaceReviewsAppService placeReviewsAppService)
    {
        _placeReviewsAppService = placeReviewsAppService;
    }

    public virtual async Task OnGetAsync()
    {
        PlaceLookupList.AddRange((await _placeReviewsAppService.GetPlaceLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}