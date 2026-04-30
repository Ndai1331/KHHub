using KHHub.MasterDataService.Services.Dtos.Articles;
using KHHub.MasterDataService.Entities.Articles;
using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.ArticleViews;

public abstract class ArticleViewWithNavigationPropertiesDtoBase
{
    public ArticleViewDto ArticleView { get; set; } = null!;
    public ArticleDto Article { get; set; } = null!;
}