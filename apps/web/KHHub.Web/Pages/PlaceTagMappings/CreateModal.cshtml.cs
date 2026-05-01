using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.PlaceTagMappings;
using KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;

namespace KHHub.Web.Pages.PlaceTagMappings;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public PlaceTagMappingCreateViewModel PlaceTagMapping { get; set; }

    protected IPlaceTagMappingsAppService _placeTagMappingsAppService;

    public CreateModalModelBase(IPlaceTagMappingsAppService placeTagMappingsAppService)
    {
        _placeTagMappingsAppService = placeTagMappingsAppService;
        PlaceTagMapping = new();
    }

    public virtual async Task OnGetAsync()
    {
        PlaceTagMapping = new PlaceTagMappingCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _placeTagMappingsAppService.CreateAsync(ObjectMapper.Map<PlaceTagMappingCreateViewModel, PlaceTagMappingCreateDto>(PlaceTagMapping));
        return NoContent();
    }
}

public class PlaceTagMappingCreateViewModel : PlaceTagMappingCreateDto
{
}