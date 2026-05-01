using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.Places;
using KHHub.MasterDataService.Services.Dtos.Places;

namespace KHHub.Web.Pages.Places;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public PlaceCreateViewModel Place { get; set; }

    protected IPlacesAppService _placesAppService;

    public CreateModalModelBase(IPlacesAppService placesAppService)
    {
        _placesAppService = placesAppService;
        Place = new();
    }

    public virtual async Task OnGetAsync()
    {
        Place = new PlaceCreateViewModel();
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _placesAppService.CreateAsync(ObjectMapper.Map<PlaceCreateViewModel, PlaceCreateDto>(Place));
        return NoContent();
    }
}

public class PlaceCreateViewModel : PlaceCreateDto
{
}