using System;

namespace KHHub.MasterDataService.Services.Dtos.Articles;

public abstract class ArticleDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}