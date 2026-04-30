using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.ArticleViews;
using KHHub.MasterDataService.Services.Dtos.ArticleViews;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.ArticleViews;

public abstract class IndexModelBase : AbpPageModel
{
    public string? IpAddressFilter { get; set; }

    public string? DeviceFilter { get; set; }

    public string? SourceFilter { get; set; }

    public DateTime? ViewedAtFilterMin { get; set; }

    public DateTime? ViewedAtFilterMax { get; set; }

    public int? DurationFilterMin { get; set; }

    public int? DurationFilterMax { get; set; }

    public string? UserIdFilter { get; set; }

    [SelectItems(nameof(ArticleLookupList))]
    public Guid ArticleIdFilter { get; set; }

    public List<SelectListItem> ArticleLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IArticleViewsAppService _articleViewsAppService;

    public IndexModelBase(IArticleViewsAppService articleViewsAppService)
    {
        _articleViewsAppService = articleViewsAppService;
    }

    public virtual async Task OnGetAsync()
    {
        ArticleLookupList.AddRange((await _articleViewsAppService.GetArticleLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}