using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.ArticleTagMappings;
using KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.Web.Pages.ArticleTagMappings;

public abstract class IndexModelBase : AbpPageModel
{
    [SelectItems(nameof(IsPrimaryBoolFilterItems))]
    public string IsPrimaryFilter { get; set; }

    public List<SelectListItem> IsPrimaryBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    public int? OrderFilterMin { get; set; }

    public int? OrderFilterMax { get; set; }

    [SelectItems(nameof(ArticleTagLookupList))]
    public Guid ArticleTagIdFilter { get; set; }

    public List<SelectListItem> ArticleTagLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };
    [SelectItems(nameof(ArticleLookupList))]
    public Guid ArticleIdFilter { get; set; }

    public List<SelectListItem> ArticleLookupList { get; set; } = new List<SelectListItem> { new SelectListItem(string.Empty, "") };

    protected IArticleTagMappingsAppService _articleTagMappingsAppService;

    public IndexModelBase(IArticleTagMappingsAppService articleTagMappingsAppService)
    {
        _articleTagMappingsAppService = articleTagMappingsAppService;
    }

    public virtual async Task OnGetAsync()
    {
        ArticleTagLookupList.AddRange((await _articleTagMappingsAppService.GetArticleTagLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        ArticleLookupList.AddRange((await _articleTagMappingsAppService.GetArticleLookupAsync(new LookupRequestDto { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items.Select(t => new SelectListItem(t.DisplayName, t.Id.ToString())).ToList());
        await Task.CompletedTask;
    }
}