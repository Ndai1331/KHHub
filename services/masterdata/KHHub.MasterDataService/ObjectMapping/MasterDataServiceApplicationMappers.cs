using KHHub.MasterDataService.Services.Dtos.MediaFiles;
using KHHub.MasterDataService.Entities.MediaFiles;
using KHHub.MasterDataService.Services.Dtos.ArticleViews;
using KHHub.MasterDataService.Entities.ArticleViews;
using KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;
using KHHub.MasterDataService.Entities.ArticleTagMappings;
using KHHub.MasterDataService.Services.Dtos.Articles;
using KHHub.MasterDataService.Entities.Articles;
using KHHub.MasterDataService.Services.Dtos.ArticleTags;
using KHHub.MasterDataService.Entities.ArticleTags;
using KHHub.MasterDataService.Services.Dtos.ArticleCategories;
using KHHub.MasterDataService.Entities.ArticleCategories;
using KHHub.MasterDataService.Services.Dtos.Wards;
using KHHub.MasterDataService.Entities.Wards;
using System;
using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Services.Dtos.Provinces;
using KHHub.MasterDataService.Entities.Provinces;
using System.Linq;
using System.Collections.Generic;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace KHHub.MasterDataService.ObjectMapping;

/*
Write your mappings here...

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class MasterDataServiceApplicationMappers : MapperBase<Book, BookDto>
{
    public override partial BookDto Map(Book source);

    public override partial void Map(Book source, BookDto destination);
}
*/
[Mapper]
public partial class ProvinceToProvinceDtoMappers : MapperBase<Province, ProvinceDto>
{
    public override partial ProvinceDto Map(Province source);
    public override partial void Map(Province source, ProvinceDto destination);
}

[Mapper]
public partial class ProvinceToProvinceExcelDtoMappers : MapperBase<Province, ProvinceExcelDto>
{
    public override partial ProvinceExcelDto Map(Province source);
    public override partial void Map(Province source, ProvinceExcelDto destination);
}

[Mapper]
public partial class WardToWardDtoMappers : MapperBase<Ward, WardDto>
{
    public override partial WardDto Map(Ward source);
    public override partial void Map(Ward source, WardDto destination);
}

[Mapper]
public partial class WardToWardExcelDtoMappers : MapperBase<Ward, WardExcelDto>
{
    public override partial WardExcelDto Map(Ward source);
    public override partial void Map(Ward source, WardExcelDto destination);
}

[Mapper]
public partial class WardWithNavigationPropertiesToWardWithNavigationPropertiesDtoMapper : MapperBase<WardWithNavigationProperties, WardWithNavigationPropertiesDto>
{
    public override partial WardWithNavigationPropertiesDto Map(WardWithNavigationProperties source);
    public override partial void Map(WardWithNavigationProperties source, WardWithNavigationPropertiesDto destination);
}

[Mapper]
public partial class ProvinceToLookupDtoGuidMapper : MapperBase<Province, LookupDto<Guid>>
{
    public override partial LookupDto<Guid> Map(Province source);
    public override partial void Map(Province source, LookupDto<Guid> destination);

    public override void AfterMap(Province source, LookupDto<Guid> destination)
    {
        destination.DisplayName = source.Name;
    }
}

[Mapper]
public partial class ArticleCategoryToArticleCategoryDtoMappers : MapperBase<ArticleCategory, ArticleCategoryDto>
{
    public override partial ArticleCategoryDto Map(ArticleCategory source);
    public override partial void Map(ArticleCategory source, ArticleCategoryDto destination);
}

[Mapper]
public partial class ArticleCategoryToArticleCategoryExcelDtoMappers : MapperBase<ArticleCategory, ArticleCategoryExcelDto>
{
    public override partial ArticleCategoryExcelDto Map(ArticleCategory source);
    public override partial void Map(ArticleCategory source, ArticleCategoryExcelDto destination);
}

[Mapper]
public partial class ArticleTagToArticleTagDtoMappers : MapperBase<ArticleTag, ArticleTagDto>
{
    public override partial ArticleTagDto Map(ArticleTag source);
    public override partial void Map(ArticleTag source, ArticleTagDto destination);
}

[Mapper]
public partial class ArticleTagToArticleTagExcelDtoMappers : MapperBase<ArticleTag, ArticleTagExcelDto>
{
    public override partial ArticleTagExcelDto Map(ArticleTag source);
    public override partial void Map(ArticleTag source, ArticleTagExcelDto destination);
}

[Mapper]
public partial class ArticleToArticleDtoMappers : MapperBase<Article, ArticleDto>
{
    public override partial ArticleDto Map(Article source);
    public override partial void Map(Article source, ArticleDto destination);
}

[Mapper]
public partial class ArticleToArticleExcelDtoMappers : MapperBase<Article, ArticleExcelDto>
{
    public override partial ArticleExcelDto Map(Article source);
    public override partial void Map(Article source, ArticleExcelDto destination);
}

[Mapper]
public partial class ArticleWithNavigationPropertiesToArticleWithNavigationPropertiesDtoMapper : MapperBase<ArticleWithNavigationProperties, ArticleWithNavigationPropertiesDto>
{
    public override partial ArticleWithNavigationPropertiesDto Map(ArticleWithNavigationProperties source);
    public override partial void Map(ArticleWithNavigationProperties source, ArticleWithNavigationPropertiesDto destination);
}

[Mapper]
public partial class ArticleCategoryToLookupDtoGuidMapper : MapperBase<ArticleCategory, LookupDto<Guid>>
{
    public override partial LookupDto<Guid> Map(ArticleCategory source);
    public override partial void Map(ArticleCategory source, LookupDto<Guid> destination);

    public override void AfterMap(ArticleCategory source, LookupDto<Guid> destination)
    {
        destination.DisplayName = source.Name;
    }
}

[Mapper]
public partial class ArticleTagMappingToArticleTagMappingDtoMappers : MapperBase<ArticleTagMapping, ArticleTagMappingDto>
{
    public override partial ArticleTagMappingDto Map(ArticleTagMapping source);
    public override partial void Map(ArticleTagMapping source, ArticleTagMappingDto destination);
}

[Mapper]
public partial class ArticleTagMappingToArticleTagMappingExcelDtoMappers : MapperBase<ArticleTagMapping, ArticleTagMappingExcelDto>
{
    public override partial ArticleTagMappingExcelDto Map(ArticleTagMapping source);
    public override partial void Map(ArticleTagMapping source, ArticleTagMappingExcelDto destination);
}

[Mapper]
public partial class ArticleTagMappingWithNavigationPropertiesToArticleTagMappingWithNavigationPropertiesDtoMapper : MapperBase<ArticleTagMappingWithNavigationProperties, ArticleTagMappingWithNavigationPropertiesDto>
{
    public override partial ArticleTagMappingWithNavigationPropertiesDto Map(ArticleTagMappingWithNavigationProperties source);
    public override partial void Map(ArticleTagMappingWithNavigationProperties source, ArticleTagMappingWithNavigationPropertiesDto destination);
}

[Mapper]
public partial class ArticleTagToLookupDtoGuidMapper : MapperBase<ArticleTag, LookupDto<Guid>>
{
    public override partial LookupDto<Guid> Map(ArticleTag source);
    public override partial void Map(ArticleTag source, LookupDto<Guid> destination);

    public override void AfterMap(ArticleTag source, LookupDto<Guid> destination)
    {
        destination.DisplayName = source.Slug;
    }
}

[Mapper]
public partial class ArticleToLookupDtoGuidMapper : MapperBase<Article, LookupDto<Guid>>
{
    public override partial LookupDto<Guid> Map(Article source);
    public override partial void Map(Article source, LookupDto<Guid> destination);

    public override void AfterMap(Article source, LookupDto<Guid> destination)
    {
        destination.DisplayName = source.Title;
    }
}

[Mapper]
public partial class ArticleViewToArticleViewDtoMappers : MapperBase<ArticleView, ArticleViewDto>
{
    public override partial ArticleViewDto Map(ArticleView source);
    public override partial void Map(ArticleView source, ArticleViewDto destination);
}

[Mapper]
public partial class ArticleViewToArticleViewExcelDtoMappers : MapperBase<ArticleView, ArticleViewExcelDto>
{
    public override partial ArticleViewExcelDto Map(ArticleView source);
    public override partial void Map(ArticleView source, ArticleViewExcelDto destination);
}

[Mapper]
public partial class ArticleViewWithNavigationPropertiesToArticleViewWithNavigationPropertiesDtoMapper : MapperBase<ArticleViewWithNavigationProperties, ArticleViewWithNavigationPropertiesDto>
{
    public override partial ArticleViewWithNavigationPropertiesDto Map(ArticleViewWithNavigationProperties source);
    public override partial void Map(ArticleViewWithNavigationProperties source, ArticleViewWithNavigationPropertiesDto destination);
}

[Mapper]
public partial class MediaFileToMediaFileDtoMappers : MapperBase<MediaFile, MediaFileDto>
{
    public override partial MediaFileDto Map(MediaFile source);
    public override partial void Map(MediaFile source, MediaFileDto destination);
}

[Mapper]
public partial class MediaFileToMediaFileExcelDtoMappers : MapperBase<MediaFile, MediaFileExcelDto>
{
    public override partial MediaFileExcelDto Map(MediaFile source);
    public override partial void Map(MediaFile source, MediaFileExcelDto destination);
}