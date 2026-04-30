using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.Wards;
using KHHub.MasterDataService.Services.Dtos.Wards;

namespace KHHub.Web.Pages.Wards;

public abstract class CreateModalModelBase : AbpPageModel
{
    [BindProperty]
    public WardCreateViewModel Ward { get; set; }

    public List<SelectListItem> ProvinceLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IWardsAppService _wardsAppService;

    public CreateModalModelBase(IWardsAppService wardsAppService)
    {
        _wardsAppService = wardsAppService;
        Ward = new();
    }

    public virtual async Task OnGetAsync()
    {
        Ward = new WardCreateViewModel();
        ProvinceLookupListRequired.AddRange((await _wardsAppService.GetProvinceLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }

    public virtual async Task<IActionResult> OnPostAsync()
    {
        await _wardsAppService.CreateAsync(ObjectMapper.Map<WardCreateViewModel, WardCreateDto>(Ward));
        return NoContent();
    }
}

public class WardCreateViewModel : WardCreateDto
{
}