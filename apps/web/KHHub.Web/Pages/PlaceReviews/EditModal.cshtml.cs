using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Places;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.PlaceReviews;
using KHHub.MasterDataService.Services.Dtos.PlaceReviews;
using KHHub.MasterDataService.Services.Dtos.Places;

namespace KHHub.Web.Pages.PlaceReviews;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public PlaceReviewUpdateViewModel PlaceReview { get; set; }

    public PlaceDto Place { get; set; }

    protected IPlaceReviewsAppService _placeReviewsAppService;

    public EditModalModelBase(IPlaceReviewsAppService placeReviewsAppService)
    {
        _placeReviewsAppService = placeReviewsAppService;
        PlaceReview = new();
    }

    public virtual async Task OnGetAsync()
    {
        var placeReviewWithNavigationPropertiesDto = await _placeReviewsAppService.GetWithNavigationPropertiesAsync(Id);
        PlaceReview = ObjectMapper.Map<PlaceReviewDto, PlaceReviewUpdateViewModel>(placeReviewWithNavigationPropertiesDto.PlaceReview);
        Place = placeReviewWithNavigationPropertiesDto.Place;
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _placeReviewsAppService.UpdateAsync(Id, ObjectMapper.Map<PlaceReviewUpdateViewModel, PlaceReviewUpdateDto>(PlaceReview));
        return NoContent();
    }
}

public class PlaceReviewUpdateViewModel : PlaceReviewUpdateDto
{
}