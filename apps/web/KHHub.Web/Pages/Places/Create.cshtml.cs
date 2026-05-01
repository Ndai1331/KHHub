using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Localization;
using KHHub.MasterDataService.Permissions;
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

    private readonly IStringLocalizer<MasterDataServiceResource> _masterDataLocalizer;

    public CreateModel(IStringLocalizer<MasterDataServiceResource> masterDataLocalizer)
    {
        _masterDataLocalizer = masterDataLocalizer;
    }

    public Task OnGetAsync()
    {
        FillEnumLookups();
        return Task.CompletedTask;
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
