using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.ArticleCategories;
using KHHub.MasterDataService.Services.Dtos.ArticleCategories;

namespace KHHub.Web.Pages.ArticleCategories;

public abstract class IndexModelBase : AbpPageModel
{
    public string? NameFilter { get; set; }

    public string? SlugFilter { get; set; }

    public string? DescriptionFilter { get; set; }

    public string? IconFilter { get; set; }

    public string? ParentIdFilter { get; set; }

    public int? DisplayOrderFilterMin { get; set; }

    public int? DisplayOrderFilterMax { get; set; }

    [SelectItems(nameof(IsActiveBoolFilterItems))]
    public string IsActiveFilter { get; set; }

    public List<SelectListItem> IsActiveBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    public string? ThumbnailUrlFilter { get; set; }

    protected IArticleCategoriesAppService _articleCategoriesAppService;

    public IndexModelBase(IArticleCategoriesAppService articleCategoriesAppService)
    {
        _articleCategoriesAppService = articleCategoriesAppService;
    }

    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}