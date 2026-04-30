using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleTags;

public abstract class ArticleTagDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}