using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.PlaceCategories;
using KHHub.MasterDataService.Services.Dtos.PlaceCategories;

namespace KHHub.Web.Pages.PlaceCategories;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public PlaceCategoryUpdateViewModel PlaceCategory { get; set; }

    protected IPlaceCategoriesAppService _placeCategoriesAppService;

    public EditModalModelBase(IPlaceCategoriesAppService placeCategoriesAppService)
    {
        _placeCategoriesAppService = placeCategoriesAppService;
        PlaceCategory = new();
    }

    public virtual async Task OnGetAsync()
    {
        var placeCategory = await _placeCategoriesAppService.GetAsync(Id);
        PlaceCategory = ObjectMapper.Map<PlaceCategoryDto, PlaceCategoryUpdateViewModel>(placeCategory);
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _placeCategoriesAppService.UpdateAsync(Id, ObjectMapper.Map<PlaceCategoryUpdateViewModel, PlaceCategoryUpdateDto>(PlaceCategory));
        return NoContent();
    }
}

public class PlaceCategoryUpdateViewModel : PlaceCategoryUpdateDto
{
}