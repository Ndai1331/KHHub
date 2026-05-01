using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using KHHub.MasterDataService.Services.Articles;
using KHHub.MasterDataService.Services.Dtos.Articles;
using Microsoft.Extensions.Localization;

namespace KHHub.Web.Pages.Articles;

public abstract class IndexModelBase : AbpPageModel
{
    public string? TitleFilter { get; set; }

    public string? SlugFilter { get; set; }

    public string? SummaryFilter { get; set; }

    public string? ContentFilter { get; set; }

    public string? ThumbnailUrlFilter { get; set; }

    public string? CoverImageUrlFilter { get; set; }

    public ArticleType? TypeFilter { get; set; }
    
    public List<SelectListItem> ArticleTypeList { get; set; } = [];

    public string? AuthorNameFilter { get; set; }

    public string? SourceFilter { get; set; }

    public string? SourceUrlFilter { get; set; }

    public ArticleStatus? StatusFilter { get; set; }
    
    public List<SelectListItem> ArticleStatusList { get; set; } = [];

    public DateTime? PublishedAtFilterMin { get; set; }

    public DateTime? PublishedAtFilterMax { get; set; }

    [SelectItems(nameof(IsFeaturedBoolFilterItems))]
    public string IsFeaturedFilter { get; set; }

    public List<SelectListItem> IsFeaturedBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    [SelectItems(nameof(IsHotBoolFilterItems))]
    public string IsHotFilter { get; set; }

    public List<SelectListItem> IsHotBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    [SelectItems(nameof(IsTrendingBoolFilterItems))]
    public string IsTrendingFilter { get; set; }

    public List<SelectListItem> IsTrendingBoolFilterItems { get; set; } = new List<SelectListItem> { new SelectListItem("", ""),
        new SelectListItem("Yes", "true"),
        new SelectListItem("No", "false"),
    };
    public int? ViewCountFilterMin { get; set; }

    public int? ViewCountFilterMax { get; set; }

    public int? LikeCountFilterMin { get; set; }

    public int? LikeCountFilterMax { get; set; }

    public int? ShareCountFilterMin { get; set; }

    public int? ShareCountFilterMax { get; set; }

    public int? CommentCountFilterMin { get; set; }

    public int? CommentCountFilterMax { get; set; }

    public int? ReadingTimeFilterMin { get; set; }

    public int? ReadingTimeFilterMax { get; set; }

    public string? SeoTitleFilter { get; set; }

    public string? SeoDescriptionFilter { get; set; }

    public string? SeoKeywordsFilter { get; set; }

    public Guid? ArticleCategoryIdFilter { get; set; }

    protected IArticlesAppService _articlesAppService;
    protected IStringLocalizer<MasterDataServiceResource> _masterDataLocalizer;

    public IndexModelBase(
        IArticlesAppService articlesAppService,
        IStringLocalizer<MasterDataServiceResource> masterDataLocalizer)
    {
        _articlesAppService = articlesAppService;
        _masterDataLocalizer = masterDataLocalizer;
    }

    public virtual async Task OnGetAsync()
    {
        ArticleTypeList =
        [
            new SelectListItem(_masterDataLocalizer["All"].Value, string.Empty)
        ];
        ArticleTypeList.AddRange(
            Enum.GetValues<ArticleType>()
                .Select(t => new SelectListItem(_masterDataLocalizer[$"Enum:{nameof(ArticleType)}.{(int)t}"].Value, ((int)t).ToString()))
        );

        ArticleStatusList =
        [
            new SelectListItem(_masterDataLocalizer["All"].Value, string.Empty)
        ];
        ArticleStatusList.AddRange(
            Enum.GetValues<ArticleStatus>()
                .Select(s => new SelectListItem(_masterDataLocalizer[$"Enum:{nameof(ArticleStatus)}.{(int)s}"].Value, ((int)s).ToString()))
        );

        await Task.CompletedTask;
    }
}