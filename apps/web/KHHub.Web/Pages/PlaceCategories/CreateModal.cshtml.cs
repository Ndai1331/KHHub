using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.PlaceCategories;
using KHHub.MasterDataService.Services.Dtos.PlaceCategories;

namespace KHHub.Web.Pages.PlaceCategories;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public PlaceCategoryCreateViewModel PlaceCategory { get; set; }

    protected IPlaceCategoriesAppService _placeCategoriesAppService;

    public CreateModalModelBase(IPlaceCategoriesAppService placeCategoriesAppService)
    {
        _placeCategoriesAppService = placeCategoriesAppService;
        PlaceCategory = new();
    }

    public virtual async Task OnGetAsync()
    {
        PlaceCategory = new PlaceCategoryCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _placeCategoriesAppService.CreateAsync(ObjectMapper.Map<PlaceCategoryCreateViewModel, PlaceCategoryCreateDto>(PlaceCategory));
        return NoContent();
    }
}

public class PlaceCategoryCreateViewModel : PlaceCategoryCreateDto
{
}