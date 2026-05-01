using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using KHHub.MasterDataService.Localization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace KHHub.Web.Pages.ArticleTagMappings;

public abstract class IndexModelBase : AbpPageModel
{
    public Guid? ArticleIdFilter { get; set; }

    public Guid? ArticleTagIdFilter { get; set; }

    public string IsPrimaryFilter { get; set; } = string.Empty;

    public List<SelectListItem> IsPrimaryFilterItems { get; set; } = [];

    protected IStringLocalizer<MasterDataServiceResource> _masterDataLocalizer;

    public IndexModelBase(IStringLocalizer<MasterDataServiceResource> masterDataLocalizer)
    {
        _masterDataLocalizer = masterDataLocalizer;
    }

    public virtual async Task OnGetAsync()
    {
        IsPrimaryFilterItems =
        [
            new SelectListItem(_masterDataLocalizer["All"].Value, string.Empty),
            new SelectListItem(_masterDataLocalizer["Yes"].Value, "true"),
            new SelectListItem(_masterDataLocalizer["No"].Value, "false"),
        ];
        await Task.CompletedTask;
    }
}
