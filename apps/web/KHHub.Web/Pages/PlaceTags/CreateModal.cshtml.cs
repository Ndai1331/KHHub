using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.PlaceTags;
using KHHub.MasterDataService.Services.Dtos.PlaceTags;

namespace KHHub.Web.Pages.PlaceTags;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public PlaceTagCreateViewModel PlaceTag { get; set; }

    protected IPlaceTagsAppService _placeTagsAppService;

    public CreateModalModelBase(IPlaceTagsAppService placeTagsAppService)
    {
        _placeTagsAppService = placeTagsAppService;
        PlaceTag = new();
    }

    public virtual async Task OnGetAsync()
    {
        PlaceTag = new PlaceTagCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _placeTagsAppService.CreateAsync(ObjectMapper.Map<PlaceTagCreateViewModel, PlaceTagCreateDto>(PlaceTag));
        return NoContent();
    }
}

public class PlaceTagCreateViewModel : PlaceTagCreateDto
{
}