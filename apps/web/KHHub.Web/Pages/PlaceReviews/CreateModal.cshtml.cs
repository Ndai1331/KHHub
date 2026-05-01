using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.PlaceReviews;
using KHHub.MasterDataService.Services.Dtos.PlaceReviews;

namespace KHHub.Web.Pages.PlaceReviews;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public PlaceReviewCreateViewModel PlaceReview { get; set; }

    protected IPlaceReviewsAppService _placeReviewsAppService;

    public CreateModalModelBase(IPlaceReviewsAppService placeReviewsAppService)
    {
        _placeReviewsAppService = placeReviewsAppService;
        PlaceReview = new();
    }

    public virtual async Task OnGetAsync()
    {
        PlaceReview = new PlaceReviewCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _placeReviewsAppService.CreateAsync(ObjectMapper.Map<PlaceReviewCreateViewModel, PlaceReviewCreateDto>(PlaceReview));
        return NoContent();
    }
}

public class PlaceReviewCreateViewModel : PlaceReviewCreateDto
{
}