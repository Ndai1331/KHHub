using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.PlaceFavorites;
using KHHub.MasterDataService.Services.Dtos.PlaceFavorites;

namespace KHHub.Web.Pages.PlaceFavorites;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public PlaceFavoriteCreateViewModel PlaceFavorite { get; set; }

    protected IPlaceFavoritesAppService _placeFavoritesAppService;

    public CreateModalModelBase(IPlaceFavoritesAppService placeFavoritesAppService)
    {
        _placeFavoritesAppService = placeFavoritesAppService;
        PlaceFavorite = new();
    }

    public virtual async Task OnGetAsync()
    {
        PlaceFavorite = new PlaceFavoriteCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _placeFavoritesAppService.CreateAsync(ObjectMapper.Map<PlaceFavoriteCreateViewModel, PlaceFavoriteCreateDto>(PlaceFavorite));
        return NoContent();
    }
}

public class PlaceFavoriteCreateViewModel : PlaceFavoriteCreateDto
{
}