using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.PlaceTagMappings;
using KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.PlaceTagMappings;

public abstract class IndexModelBase : AbpPageModel
{
    [SelectItems(nameof(IsPrimaryBoolFilterItems))]
    public string IsPrimaryFilter { get; set; }

    public List<SelectListItem> IsPrimaryBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    public int? SortOrderFilterMin { get; set; }

    public int? SortOrderFilterMax { get; set; }

    [SelectItems(nameof(PlaceTagLookupList))]
    public Guid PlaceTagIdFilter { get; set; }

    public List<SelectListItem> PlaceTagLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };
    [SelectItems(nameof(PlaceLookupList))]
    public Guid PlaceIdFilter { get; set; }

    public List<SelectListItem> PlaceLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IPlaceTagMappingsAppService _placeTagMappingsAppService;

    public IndexModelBase(IPlaceTagMappingsAppService placeTagMappingsAppService)
    {
        _placeTagMappingsAppService = placeTagMappingsAppService;
    }

    public virtual async Task OnGetAsync()
    {
        PlaceTagLookupList.AddRange((await _placeTagMappingsAppService.GetPlaceTagLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        PlaceLookupList.AddRange((await _placeTagMappingsAppService.GetPlaceLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}