using System;

namespace KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;

public abstract class ArticleTagMappingDownloadTokenCacheItemBase
{
    public string Token { get; set; } = null!;
}