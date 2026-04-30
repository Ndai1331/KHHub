using KHHub.MasterDataService.Entities.Articles;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.ArticleViews;

namespace KHHub.MasterDataService.Entities.ArticleViews;

public abstract class ArticleViewWithNavigationPropertiesBase
{
    public ArticleView ArticleView { get; set; } = null!;
    public Article Article { get; set; } = null!;
}