using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Entities.PlaceTags;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.PlaceTagMappings;
using KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;

namespace KHHub.Web.Pages.PlaceTagMappings;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public PlaceTagMappingUpdateViewModel PlaceTagMapping { get; set; }

    public PlaceTagDto PlaceTag { get; set; }

    public PlaceDto Place { get; set; }

    protected IPlaceTagMappingsAppService _placeTagMappingsAppService;

    public EditModalModelBase(IPlaceTagMappingsAppService placeTagMappingsAppService)
    {
        _placeTagMappingsAppService = placeTagMappingsAppService;
        PlaceTagMapping = new();
    }

    public virtual async Task OnGetAsync()
    {
        var placeTagMappingWithNavigationPropertiesDto = await _placeTagMappingsAppService.GetWithNavigationPropertiesAsync(Id);
        PlaceTagMapping = ObjectMapper.Map<PlaceTagMappingDto, PlaceTagMappingUpdateViewModel>(placeTagMappingWithNavigationPropertiesDto.PlaceTagMapping);
        PlaceTag = placeTagMappingWithNavigationPropertiesDto.PlaceTag;
        Place = placeTagMappingWithNavigationPropertiesDto.Place;
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _placeTagMappingsAppService.UpdateAsync(Id, ObjectMapper.Map<PlaceTagMappingUpdateViewModel, PlaceTagMappingUpdateDto>(PlaceTagMapping));
        return NoContent();
    }
}

public class PlaceTagMappingUpdateViewModel : PlaceTagMappingUpdateDto
{
}