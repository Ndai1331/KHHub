using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Localization;
using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.MasterDataService.Services.Provinces;
using KHHub.Web.Address;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.Places;

[Authorize(MasterDataServicePermissions.Places.Create)]
public class CreateModel : AbpPageModel
{
    [BindProperty]
    public PlaceCreateViewModel Place { get; set; } = new();

    public List<SelectListItem> PlaceStatusList { get; set; } = [];

    public List<SelectListItem> PriceRangeList { get; set; } = [];

    public Guid DefaultProvinceId { get; set; }

    public string DefaultProvinceDisplayName { get; set; } = "";

    private readonly IStringLocalizer<MasterDataServiceResource> _masterDataLocalizer;

    private readonly IProvincesAppService _provincesAppService;

    public CreateModel(
        IStringLocalizer<MasterDataServiceResource> masterDataLocalizer,
        IProvincesAppService provincesAppService)
    {
        _masterDataLocalizer = masterDataLocalizer;
        _provincesAppService = provincesAppService;
    }

    public async Task OnGetAsync()
    {
        FillEnumLookups();

        var prov = await KhHubDefaultProvinceAddress.TryGetAsync(_provincesAppService);
        if (prov != null)
        {
            DefaultProvinceId = prov.Value.Id;
            DefaultProvinceDisplayName = prov.Value.Name;
            Place.ProvinceId = prov.Value.Id;
        }
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

public class PlaceCreateViewModel : PlaceCreateDto
{
}
