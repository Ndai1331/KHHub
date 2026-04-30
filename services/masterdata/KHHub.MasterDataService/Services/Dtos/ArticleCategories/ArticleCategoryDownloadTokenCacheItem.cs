using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleCategories;

public abstract class ArticleCategoryDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}