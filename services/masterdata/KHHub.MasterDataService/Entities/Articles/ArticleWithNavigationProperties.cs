using KHHub.MasterDataService.Entities.ArticleCategories;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.Articles;

namespace KHHub.MasterDataService.Entities.Articles;

public abstract class ArticleWithNavigationPropertiesBase
{
    public Article Article { get; set; } = null!;
    public ArticleCategory ArticleCategory { get; set; } = null!;
}