using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.PlaceViews;
using KHHub.MasterDataService.Services.Dtos.PlaceViews;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.PlaceViews;

public abstract class IndexModelBase : AbpPageModel
{
    public string? UserIdFilter { get; set; }

    public string? IpAddressFilter { get; set; }

    public string? DeviceFilter { get; set; }

    public DateTime? ViewedAtFilterMin { get; set; }

    public DateTime? ViewedAtFilterMax { get; set; }

    public int? DurationFilterMin { get; set; }

    public int? DurationFilterMax { get; set; }

    public string? SourceFilter { get; set; }

    [SelectItems(nameof(PlaceLookupList))]
    public Guid PlaceIdFilter { get; set; }

    public List<SelectListItem> PlaceLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IPlaceViewsAppService _placeViewsAppService;

    public IndexModelBase(IPlaceViewsAppService placeViewsAppService)
    {
        _placeViewsAppService = placeViewsAppService;
    }

    public virtual async Task OnGetAsync()
    {
        PlaceLookupList.AddRange((await _placeViewsAppService.GetPlaceLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}