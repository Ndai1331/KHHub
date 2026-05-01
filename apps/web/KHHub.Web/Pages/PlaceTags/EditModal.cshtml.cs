using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.PlaceTags;
using KHHub.MasterDataService.Services.Dtos.PlaceTags;

namespace KHHub.Web.Pages.PlaceTags;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public PlaceTagUpdateViewModel PlaceTag { get; set; }

    protected IPlaceTagsAppService _placeTagsAppService;

    public EditModalModelBase(IPlaceTagsAppService placeTagsAppService)
    {
        _placeTagsAppService = placeTagsAppService;
        PlaceTag = new();
    }

    public virtual async Task OnGetAsync()
    {
        var placeTag = await _placeTagsAppService.GetAsync(Id);
        PlaceTag = ObjectMapper.Map<PlaceTagDto, PlaceTagUpdateViewModel>(placeTag);
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _placeTagsAppService.UpdateAsync(Id, ObjectMapper.Map<PlaceTagUpdateViewModel, PlaceTagUpdateDto>(PlaceTag));
        return NoContent();
    }
}

public class PlaceTagUpdateViewModel : PlaceTagUpdateDto
{
}