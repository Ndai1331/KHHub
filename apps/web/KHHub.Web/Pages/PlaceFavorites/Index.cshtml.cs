using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.PlaceFavorites;
using KHHub.MasterDataService.Services.Dtos.PlaceFavorites;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.PlaceFavorites;

public abstract class IndexModelBase : AbpPageModel
{
    public string? UserIdFilter { get; set; }

    [SelectItems(nameof(PlaceLookupList))]
    public Guid PlaceIdFilter { get; set; }

    public List<SelectListItem> PlaceLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IPlaceFavoritesAppService _placeFavoritesAppService;

    public IndexModelBase(IPlaceFavoritesAppService placeFavoritesAppService)
    {
        _placeFavoritesAppService = placeFavoritesAppService;
    }

    public virtual async Task OnGetAsync()
    {
        PlaceLookupList.AddRange((await _placeFavoritesAppService.GetPlaceLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}