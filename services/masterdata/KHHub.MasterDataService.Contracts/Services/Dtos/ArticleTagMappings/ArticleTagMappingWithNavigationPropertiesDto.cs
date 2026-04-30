using KHHub.MasterDataService.Services.Dtos.Articles;
using KHHub.MasterDataService.Services.Dtos.ArticleTags;
using KHHub.MasterDataService.Entities.ArticleTags;
using KHHub.MasterDataService.Entities.Articles;
using System;
using Volo.Abp.Application.Dtos;
using System.Collections.Generic;

namespace KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;

public abstract class ArticleTagMappingWithNavigationPropertiesDtoBase
{
    public ArticleTagMappingDto ArticleTagMapping { get; set; } = null!;
    public ArticleTagDto ArticleTag { get; set; } = null!;
    public ArticleDto Article { get; set; } = null!;
}