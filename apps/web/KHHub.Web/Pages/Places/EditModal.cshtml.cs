using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Wards;
using KHHub.MasterDataService.Entities.Provinces;
using KHHub.MasterDataService.Entities.PlaceCategories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.Places;
using KHHub.MasterDataService.Services.Dtos.Places;

namespace KHHub.Web.Pages.Places;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public PlaceUpdateViewModel Place { get; set; }

    public PlaceCategoryDto PlaceCategory { get; set; }

    public ProvinceDto Province { get; set; }

    public WardDto Ward { get; set; }

    protected IPlacesAppService _placesAppService;

    public EditModalModelBase(IPlacesAppService placesAppService)
    {
        _placesAppService = placesAppService;
        Place = new();
    }

    public virtual async Task OnGetAsync()
    {
        var placeWithNavigationPropertiesDto = await _placesAppService.GetWithNavigationPropertiesAsync(Id);
        Place = ObjectMapper.Map<PlaceDto, PlaceUpdateViewModel>(placeWithNavigationPropertiesDto.Place);
        PlaceCategory = placeWithNavigationPropertiesDto.PlaceCategory;
        Province = placeWithNavigationPropertiesDto.Province;
        Ward = placeWithNavigationPropertiesDto.Ward;
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _placesAppService.UpdateAsync(Id, ObjectMapper.Map<PlaceUpdateViewModel, PlaceUpdateDto>(Place));
        return NoContent();
    }
}

public class PlaceUpdateViewModel : PlaceUpdateDto
{
}