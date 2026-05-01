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
using KHHub.MasterDataService.Services.PlaceViews;
using KHHub.MasterDataService.Services.Dtos.PlaceViews;

namespace KHHub.Web.Pages.PlaceViews;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public PlaceViewUpdateViewModel PlaceView { get; set; }

    public PlaceDto Place { get; set; }

    protected IPlaceViewsAppService _placeViewsAppService;

    public EditModalModelBase(IPlaceViewsAppService placeViewsAppService)
    {
        _placeViewsAppService = placeViewsAppService;
        PlaceView = new();
    }

    public virtual async Task OnGetAsync()
    {
        var placeViewWithNavigationPropertiesDto = await _placeViewsAppService.GetWithNavigationPropertiesAsync(Id);
        PlaceView = ObjectMapper.Map<PlaceViewDto, PlaceViewUpdateViewModel>(placeViewWithNavigationPropertiesDto.PlaceView);
        Place = placeViewWithNavigationPropertiesDto.Place;
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _placeViewsAppService.UpdateAsync(Id, ObjectMapper.Map<PlaceViewUpdateViewModel, PlaceViewUpdateDto>(PlaceView));
        return NoContent();
    }
}

public class PlaceViewUpdateViewModel : PlaceViewUpdateDto
{
}