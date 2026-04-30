using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.Wards;
using KHHub.MasterDataService.Services.Dtos.Wards;

namespace KHHub.Web.Pages.Wards;

public abstract class EditModalModelBase : AbpPageModel
{
    [HiddenInput]
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public WardUpdateViewModel Ward { get; set; }

    public List<SelectListItem> ProvinceLookupListRequired { get; set; } = new List<SelectListItem> { };

    protected IWardsAppService _wardsAppService;

    public EditModalModelBase(IWardsAppService wardsAppService)
    {
        _wardsAppService = wardsAppService;
        Ward = new();
    }

    public virtual async Task OnGetAsync()
    {
        var wardWithNavigationPropertiesDto = await _wardsAppService.GetWithNavigationPropertiesAsync(Id);
        Ward = ObjectMapper.Map<WardDto, WardUpdateViewModel>(wardWithNavigationPropertiesDto.Ward);
        ProvinceLookupListRequired.AddRange((await _wardsAppService.GetProvinceLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
    }

    public virtual async Task<NoContentResult> OnPostAsync()
    {
        await _wardsAppService.UpdateAsync(Id, ObjectMapper.Map<WardUpdateViewModel, WardUpdateDto>(Ward));
        return NoContent();
    }
}

public class WardUpdateViewModel : WardUpdateDto
{
}