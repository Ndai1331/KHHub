using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleViews;

public abstract class ArticleViewDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}