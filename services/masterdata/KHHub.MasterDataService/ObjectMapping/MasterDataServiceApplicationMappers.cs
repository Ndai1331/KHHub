using KHHub.MasterDataService.Services.Dtos.PlaceViews;
using KHHub.MasterDataService.Entities.PlaceViews;
using KHHub.MasterDataService.Services.Dtos.PlaceFavorites;
using KHHub.MasterDataService.Entities.PlaceFavorites;
using KHHub.MasterDataService.Services.Dtos.PlaceReviews;
using KHHub.MasterDataService.Entities.PlaceReviews;
using KHHub.MasterDataService.Services.Dtos.EntityFiles;
using KHHub.MasterDataService.Entities.EntityFiles;
using KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;
using KHHub.MasterDataService.Entities.PlaceTagMappings;
using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Services.Dtos.PlaceTags;
using KHHub.MasterDataService.Entities.PlaceTags;
using KHHub.MasterDataService.Services.Dtos.PlaceCategories;
using KHHub.MasterDataService.Entities.PlaceCategories;
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
        destination.DisplayName = source.Name;
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

[Mapper]
public partial class PlaceCategoryToPlaceCategoryDtoMappers : MapperBase<PlaceCategory, PlaceCategoryDto>
{
    public override partial PlaceCategoryDto Map(PlaceCategory source);
    public override partial void Map(PlaceCategory source, PlaceCategoryDto destination);
}

[Mapper]
public partial class PlaceCategoryToPlaceCategoryExcelDtoMappers : MapperBase<PlaceCategory, PlaceCategoryExcelDto>
{
    public override partial PlaceCategoryExcelDto Map(PlaceCategory source);
    public override partial void Map(PlaceCategory source, PlaceCategoryExcelDto destination);
}

[Mapper]
public partial class PlaceTagToPlaceTagDtoMappers : MapperBase<PlaceTag, PlaceTagDto>
{
    public override partial PlaceTagDto Map(PlaceTag source);
    public override partial void Map(PlaceTag source, PlaceTagDto destination);
}

[Mapper]
public partial class PlaceTagToPlaceTagExcelDtoMappers : MapperBase<PlaceTag, PlaceTagExcelDto>
{
    public override partial PlaceTagExcelDto Map(PlaceTag source);
    public override partial void Map(PlaceTag source, PlaceTagExcelDto destination);
}

[Mapper]
public partial class PlaceToPlaceDtoMappers : MapperBase<Place, PlaceDto>
{
    public override partial PlaceDto Map(Place source);
    public override partial void Map(Place source, PlaceDto destination);
}

[Mapper]
public partial class PlaceToPlaceExcelDtoMappers : MapperBase<Place, PlaceExcelDto>
{
    public override partial PlaceExcelDto Map(Place source);
    public override partial void Map(Place source, PlaceExcelDto destination);
}

[Mapper]
public partial class PlaceWithNavigationPropertiesToPlaceWithNavigationPropertiesDtoMapper : MapperBase<PlaceWithNavigationProperties, PlaceWithNavigationPropertiesDto>
{
    public override partial PlaceWithNavigationPropertiesDto Map(PlaceWithNavigationProperties source);
    public override partial void Map(PlaceWithNavigationProperties source, PlaceWithNavigationPropertiesDto destination);
}

[Mapper]
public partial class PlaceCategoryToLookupDtoGuidMapper : MapperBase<PlaceCategory, LookupDto<Guid>>
{
    public override partial LookupDto<Guid> Map(PlaceCategory source);
    public override partial void Map(PlaceCategory source, LookupDto<Guid> destination);

    public override void AfterMap(PlaceCategory source, LookupDto<Guid> destination)
    {
        destination.DisplayName = source.Name;
    }
}

[Mapper]
public partial class WardToLookupDtoGuidMapper : MapperBase<Ward, LookupDto<Guid>>
{
    public override partial LookupDto<Guid> Map(Ward source);
    public override partial void Map(Ward source, LookupDto<Guid> destination);

    public override void AfterMap(Ward source, LookupDto<Guid> destination)
    {
        destination.DisplayName = source.Name;
    }
}

[Mapper]
public partial class PlaceTagMappingToPlaceTagMappingDtoMappers : MapperBase<PlaceTagMapping, PlaceTagMappingDto>
{
    public override partial PlaceTagMappingDto Map(PlaceTagMapping source);
    public override partial void Map(PlaceTagMapping source, PlaceTagMappingDto destination);
}

[Mapper]
public partial class PlaceTagMappingToPlaceTagMappingExcelDtoMappers : MapperBase<PlaceTagMapping, PlaceTagMappingExcelDto>
{
    public override partial PlaceTagMappingExcelDto Map(PlaceTagMapping source);
    public override partial void Map(PlaceTagMapping source, PlaceTagMappingExcelDto destination);
}

[Mapper]
public partial class PlaceTagMappingWithNavigationPropertiesToPlaceTagMappingWithNavigationPropertiesDtoMapper : MapperBase<PlaceTagMappingWithNavigationProperties, PlaceTagMappingWithNavigationPropertiesDto>
{
    public override partial PlaceTagMappingWithNavigationPropertiesDto Map(PlaceTagMappingWithNavigationProperties source);
    public override partial void Map(PlaceTagMappingWithNavigationProperties source, PlaceTagMappingWithNavigationPropertiesDto destination);
}

[Mapper]
public partial class PlaceTagToLookupDtoGuidMapper : MapperBase<PlaceTag, LookupDto<Guid>>
{
    public override partial LookupDto<Guid> Map(PlaceTag source);
    public override partial void Map(PlaceTag source, LookupDto<Guid> destination);

    public override void AfterMap(PlaceTag source, LookupDto<Guid> destination)
    {
        destination.DisplayName = source.Name;
    }
}

[Mapper]
public partial class PlaceToLookupDtoGuidMapper : MapperBase<Place, LookupDto<Guid>>
{
    public override partial LookupDto<Guid> Map(Place source);
    public override partial void Map(Place source, LookupDto<Guid> destination);

    public override void AfterMap(Place source, LookupDto<Guid> destination)
    {
        destination.DisplayName = source.Name;
    }
}

[Mapper]
public partial class EntityFileToEntityFileDtoMappers : MapperBase<EntityFile, EntityFileDto>
{
    public override partial EntityFileDto Map(EntityFile source);
    public override partial void Map(EntityFile source, EntityFileDto destination);
}

[Mapper]
public partial class EntityFileToEntityFileExcelDtoMappers : MapperBase<EntityFile, EntityFileExcelDto>
{
    public override partial EntityFileExcelDto Map(EntityFile source);
    public override partial void Map(EntityFile source, EntityFileExcelDto destination);
}

[Mapper]
public partial class EntityFileWithNavigationPropertiesToEntityFileWithNavigationPropertiesDtoMapper : MapperBase<EntityFileWithNavigationProperties, EntityFileWithNavigationPropertiesDto>
{
    public override partial EntityFileWithNavigationPropertiesDto Map(EntityFileWithNavigationProperties source);
    public override partial void Map(EntityFileWithNavigationProperties source, EntityFileWithNavigationPropertiesDto destination);
}

[Mapper]
public partial class MediaFileToLookupDtoGuidMapper : MapperBase<MediaFile, LookupDto<Guid>>
{
    public override partial LookupDto<Guid> Map(MediaFile source);
    public override partial void Map(MediaFile source, LookupDto<Guid> destination);

    public override void AfterMap(MediaFile source, LookupDto<Guid> destination)
    {
        destination.DisplayName = source.FileName;
    }
}

[Mapper]
public partial class PlaceReviewToPlaceReviewDtoMappers : MapperBase<PlaceReview, PlaceReviewDto>
{
    public override partial PlaceReviewDto Map(PlaceReview source);
    public override partial void Map(PlaceReview source, PlaceReviewDto destination);
}

[Mapper]
public partial class PlaceReviewToPlaceReviewExcelDtoMappers : MapperBase<PlaceReview, PlaceReviewExcelDto>
{
    public override partial PlaceReviewExcelDto Map(PlaceReview source);
    public override partial void Map(PlaceReview source, PlaceReviewExcelDto destination);
}

[Mapper]
public partial class PlaceReviewWithNavigationPropertiesToPlaceReviewWithNavigationPropertiesDtoMapper : MapperBase<PlaceReviewWithNavigationProperties, PlaceReviewWithNavigationPropertiesDto>
{
    public override partial PlaceReviewWithNavigationPropertiesDto Map(PlaceReviewWithNavigationProperties source);
    public override partial void Map(PlaceReviewWithNavigationProperties source, PlaceReviewWithNavigationPropertiesDto destination);
}

[Mapper]
public partial class PlaceFavoriteToPlaceFavoriteDtoMappers : MapperBase<PlaceFavorite, PlaceFavoriteDto>
{
    public override partial PlaceFavoriteDto Map(PlaceFavorite source);
    public override partial void Map(PlaceFavorite source, PlaceFavoriteDto destination);
}

[Mapper]
public partial class PlaceFavoriteToPlaceFavoriteExcelDtoMappers : MapperBase<PlaceFavorite, PlaceFavoriteExcelDto>
{
    public override partial PlaceFavoriteExcelDto Map(PlaceFavorite source);
    public override partial void Map(PlaceFavorite source, PlaceFavoriteExcelDto destination);
}

[Mapper]
public partial class PlaceFavoriteWithNavigationPropertiesToPlaceFavoriteWithNavigationPropertiesDtoMapper : MapperBase<PlaceFavoriteWithNavigationProperties, PlaceFavoriteWithNavigationPropertiesDto>
{
    public override partial PlaceFavoriteWithNavigationPropertiesDto Map(PlaceFavoriteWithNavigationProperties source);
    public override partial void Map(PlaceFavoriteWithNavigationProperties source, PlaceFavoriteWithNavigationPropertiesDto destination);
}

[Mapper]
public partial class PlaceViewToPlaceViewDtoMappers : MapperBase<PlaceView, PlaceViewDto>
{
    public override partial PlaceViewDto Map(PlaceView source);
    public override partial void Map(PlaceView source, PlaceViewDto destination);
}

[Mapper]
public partial class PlaceViewToPlaceViewExcelDtoMappers : MapperBase<PlaceView, PlaceViewExcelDto>
{
    public override partial PlaceViewExcelDto Map(PlaceView source);
    public override partial void Map(PlaceView source, PlaceViewExcelDto destination);
}

[Mapper]
public partial class PlaceViewWithNavigationPropertiesToPlaceViewWithNavigationPropertiesDtoMapper : MapperBase<PlaceViewWithNavigationProperties, PlaceViewWithNavigationPropertiesDto>
{
    public override partial PlaceViewWithNavigationPropertiesDto Map(PlaceViewWithNavigationProperties source);
    public override partial void Map(PlaceViewWithNavigationProperties source, PlaceViewWithNavigationPropertiesDto destination);
}