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
using KHHub.MasterDataService.Services.PlaceFavorites;
using KHHub.MasterDataService.Services.Dtos.PlaceFavorites;
using KHHub.MasterDataService.Services.Dtos.Places;

namespace KHHub.Web.Pages.PlaceFavorites;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public PlaceFavoriteUpdateViewModel PlaceFavorite { get; set; }

    public PlaceDto Place { get; set; }

    protected IPlaceFavoritesAppService _placeFavoritesAppService;

    public EditModalModelBase(IPlaceFavoritesAppService placeFavoritesAppService)
    {
        _placeFavoritesAppService = placeFavoritesAppService;
        PlaceFavorite = new();
    }

    public virtual async Task OnGetAsync()
    {
        var placeFavoriteWithNavigationPropertiesDto = await _placeFavoritesAppService.GetWithNavigationPropertiesAsync(Id);
        PlaceFavorite = ObjectMapper.Map<PlaceFavoriteDto, PlaceFavoriteUpdateViewModel>(placeFavoriteWithNavigationPropertiesDto.PlaceFavorite);
        Place = placeFavoriteWithNavigationPropertiesDto.Place;
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _placeFavoritesAppService.UpdateAsync(Id, ObjectMapper.Map<PlaceFavoriteUpdateViewModel, PlaceFavoriteUpdateDto>(PlaceFavorite));
        return NoContent();
    }
}

public class PlaceFavoriteUpdateViewModel : PlaceFavoriteUpdateDto
{
}