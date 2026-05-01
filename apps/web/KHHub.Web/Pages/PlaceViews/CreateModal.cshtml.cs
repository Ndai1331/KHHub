using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.PlaceViews;
using KHHub.MasterDataService.Services.Dtos.PlaceViews;

namespace KHHub.Web.Pages.PlaceViews;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public PlaceViewCreateViewModel PlaceView { get; set; }

    protected IPlaceViewsAppService _placeViewsAppService;

    public CreateModalModelBase(IPlaceViewsAppService placeViewsAppService)
    {
        _placeViewsAppService = placeViewsAppService;
        PlaceView = new();
    }

    public virtual async Task OnGetAsync()
    {
        PlaceView = new PlaceViewCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _placeViewsAppService.CreateAsync(ObjectMapper.Map<PlaceViewCreateViewModel, PlaceViewCreateDto>(PlaceView));
        return NoContent();
    }
}

public class PlaceViewCreateViewModel : PlaceViewCreateDto
{
}