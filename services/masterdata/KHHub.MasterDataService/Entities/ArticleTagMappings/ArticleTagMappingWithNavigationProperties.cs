using KHHub.MasterDataService.Entities.ArticleTags;
using KHHub.MasterDataService.Entities.Articles;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.ArticleTagMappings;

namespace KHHub.MasterDataService.Entities.ArticleTagMappings;

public abstract class ArticleTagMappingWithNavigationPropertiesBase
{
    public ArticleTagMapping ArticleTagMapping { get; set; } = null!;
    public ArticleTag ArticleTag { get; set; } = null!;
    public Article Article { get; set; } = null!;
}