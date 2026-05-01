using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Localization;
using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.MasterDataService.Services.Places;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.Places;

[Authorize(MasterDataServicePermissions.Places.Edit)]
public class EditModel : AbpPageModel
{
    [BindProperty(SupportsGet = true)]
    public Guid Id { get; set; }

    [BindProperty]
    public PlaceUpdateViewModel Place { get; set; } = new();

    public List<SelectListItem> PlaceStatusList { get; set; } = [];

    public List<SelectListItem> PriceRangeList { get; set; } = [];

    public string? SelectedPlaceCategoryDisplayName { get; set; }

    public string? SelectedProvinceDisplayName { get; set; }

    public string? SelectedWardDisplayName { get; set; }

    public DateTime? LastUpdatedAt { get; set; }

    public bool CanDelete { get; set; }

    private readonly IPlacesAppService _placesAppService;

    private readonly IAuthorizationService _authorizationService;

    private readonly IStringLocalizer<MasterDataServiceResource> _masterDataLocalizer;

    public EditModel(
        IPlacesAppService placesAppService,
        IAuthorizationService authorizationService,
        IStringLocalizer<MasterDataServiceResource> masterDataLocalizer)
    {
        _placesAppService = placesAppService;
        _authorizationService = authorizationService;
        _masterDataLocalizer = masterDataLocalizer;
    }

    public async Task OnGetAsync()
    {
        FillEnumLookups();

        var dto = await _placesAppService.GetWithNavigationPropertiesAsync(Id);
        Place = ObjectMapper.Map<PlaceDto, PlaceUpdateViewModel>(dto.Place);
        SelectedPlaceCategoryDisplayName = dto.PlaceCategory?.Name;
        SelectedProvinceDisplayName = dto.Province?.Name;
        SelectedWardDisplayName = dto.Ward?.Name;
        LastUpdatedAt = dto.Place.LastModificationTime ?? dto.Place.CreationTime;
        CanDelete = await _authorizationService.IsGrantedAsync(MasterDataServicePermissions.Places.Delete);
    }

    private void FillEnumLookups()
    {
        PriceRangeList = Enum.GetValues<PriceRange>()
            .Select(p => new SelectListItem(_masterDataLocalizer[$"Enum:{nameof(PriceRange)}.{(int)p}"].Value, ((int)p).ToString()))
            .ToList();

        PlaceStatusList = Enum.GetValues<PlaceStatus>()
            .Select(s => new SelectListItem(_masterDataLocalizer[$"Enum:{nameof(PlaceStatus)}.{(int)s}"].Value, ((int)s).ToString()))
            .ToList();
    }
}
