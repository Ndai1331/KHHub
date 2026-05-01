using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.PlaceCategories;
using KHHub.MasterDataService.Services.Dtos.PlaceCategories;

namespace KHHub.Web.Pages.PlaceCategories;

public abstract class IndexModelBase : AbpPageModel
{
    public string? NameFilter { get; set; }

    public string? SlugFilter { get; set; }

    public string? DescriptionFilter { get; set; }

    public string? IconFilter { get; set; }

    public string? ColorFilter { get; set; }

    public string? ParentIdFilter { get; set; }

    public int? DisplayOrderFilterMin { get; set; }

    public int? DisplayOrderFilterMax { get; set; }

    [SelectItems(nameof(IsActiveBoolFilterItems))]
    public string IsActiveFilter { get; set; }

    public List<SelectListItem> IsActiveBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };

    protected IPlaceCategoriesAppService _placeCategoriesAppService;

    public IndexModelBase(IPlaceCategoriesAppService placeCategoriesAppService)
    {
        _placeCategoriesAppService = placeCategoriesAppService;
    }

    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}