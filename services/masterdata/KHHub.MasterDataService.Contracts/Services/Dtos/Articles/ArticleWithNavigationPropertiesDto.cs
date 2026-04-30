using KHHub.MasterDataService.Services.Dtos.ArticleCategories;
using KHHub.MasterDataService.Entities.ArticleCategories;
using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.Articles;

public abstract class ArticleWithNavigationPropertiesDtoBase
{
    public ArticleDto Article { get; set; } = null!;
    public ArticleCategoryDto ArticleCategory { get; set; } = null!;
}