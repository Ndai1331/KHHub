using KHHub.MasterDataService.Services.Dtos.PlaceCategories;
using KHHub.Web.Pages.PlaceCategories;
using KHHub.MasterDataService.Services.Dtos.PlaceViews;
using KHHub.Web.Pages.PlaceViews;
using KHHub.MasterDataService.Services.Dtos.PlaceFavorites;
using KHHub.Web.Pages.PlaceFavorites;
using KHHub.MasterDataService.Services.Dtos.PlaceReviews;
using KHHub.Web.Pages.PlaceReviews;
using KHHub.MasterDataService.Services.Dtos.EntityFiles;
using KHHub.Web.Pages.EntityFiles;
using KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;
using KHHub.Web.Pages.PlaceTagMappings;
using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.Web.Pages.Places;
using KHHub.MasterDataService.Services.Dtos.PlaceTags;
using KHHub.Web.Pages.PlaceTags;
using KHHub.MasterDataService.Services.Dtos.PlaceCategories;
using KHHub.Web.Pages.PlaceCategories;
using KHHub.MasterDataService.Services.Dtos.MediaFiles;
using KHHub.Web.Pages.MediaFiles;
using KHHub.MasterDataService.Services.Dtos.ArticleViews;
using KHHub.Web.Pages.ArticleViews;
using KHHub.MasterDataService.Services.Dtos.ArticleTagMappings;
using KHHub.Web.Pages.ArticleTagMappings;
using KHHub.MasterDataService.Services.Dtos.Articles;
using KHHub.Web.Pages.Articles;
using KHHub.MasterDataService.Services.Dtos.ArticleTags;
using KHHub.Web.Pages.ArticleTags;
using KHHub.MasterDataService.Services.Dtos.ArticleCategories;
using KHHub.Web.Pages.ArticleCategories;
using KHHub.MasterDataService.Services.Dtos.Wards;
using KHHub.Web.Pages.Wards;
using KHHub.MasterDataService.Services.Dtos.Provinces;
using KHHub.Web.Pages.Provinces;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace KHHub.Web;

/*
Write your mappings here...

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class KHHubWebMappers : MapperBase<Book, BookDto>
{
    public override partial BookDto Map(Book source);

    public override partial void Map(Book source, BookDto destination);
}
*/
[Mapper]
public partial class ProvinceDtoToProvinceUpdateViewModelMapper : MapperBase<ProvinceDto, ProvinceUpdateViewModel>
{
    public override partial ProvinceUpdateViewModel Map(ProvinceDto source);
    public override partial void Map(ProvinceDto source, ProvinceUpdateViewModel destination);
}

[Mapper]
public partial class ProvinceUpdateViewModelToProvinceUpdateDto : MapperBase<ProvinceUpdateViewModel, ProvinceUpdateDto>
{
    public override partial ProvinceUpdateDto Map(ProvinceUpdateViewModel source);
    public override partial void Map(ProvinceUpdateViewModel source, ProvinceUpdateDto destination);
}

[Mapper]
public partial class ProvinceCreateViewModelToProvinceCreateDto : MapperBase<ProvinceCreateViewModel, ProvinceCreateDto>
{
    public override partial ProvinceCreateDto Map(ProvinceCreateViewModel source);
    public override partial void Map(ProvinceCreateViewModel source, ProvinceCreateDto destination);
}

[Mapper]
public partial class WardDtoToWardUpdateViewModelMapper : MapperBase<WardDto, WardUpdateViewModel>
{
    public override partial WardUpdateViewModel Map(WardDto source);
    public override partial void Map(WardDto source, WardUpdateViewModel destination);
}

[Mapper]
public partial class WardUpdateViewModelToWardUpdateDto : MapperBase<WardUpdateViewModel, WardUpdateDto>
{
    public override partial WardUpdateDto Map(WardUpdateViewModel source);
    public override partial void Map(WardUpdateViewModel source, WardUpdateDto destination);
}

[Mapper]
public partial class WardCreateViewModelToWardCreateDto : MapperBase<WardCreateViewModel, WardCreateDto>
{
    public override partial WardCreateDto Map(WardCreateViewModel source);
    public override partial void Map(WardCreateViewModel source, WardCreateDto destination);
}

[Mapper]
public partial class ArticleCategoryDtoToArticleCategoryUpdateViewModelMapper : MapperBase<ArticleCategoryDto, ArticleCategoryUpdateViewModel>
{
    public override partial ArticleCategoryUpdateViewModel Map(ArticleCategoryDto source);
    public override partial void Map(ArticleCategoryDto source, ArticleCategoryUpdateViewModel destination);
}

[Mapper]
public partial class ArticleCategoryUpdateViewModelToArticleCategoryUpdateDto : MapperBase<ArticleCategoryUpdateViewModel, ArticleCategoryUpdateDto>
{
    public override partial ArticleCategoryUpdateDto Map(ArticleCategoryUpdateViewModel source);
    public override partial void Map(ArticleCategoryUpdateViewModel source, ArticleCategoryUpdateDto destination);
}

[Mapper]
public partial class ArticleCategoryCreateViewModelToArticleCategoryCreateDto : MapperBase<ArticleCategoryCreateViewModel, ArticleCategoryCreateDto>
{
    public override partial ArticleCategoryCreateDto Map(ArticleCategoryCreateViewModel source);
    public override partial void Map(ArticleCategoryCreateViewModel source, ArticleCategoryCreateDto destination);
}

[Mapper]
public partial class ArticleTagDtoToArticleTagUpdateViewModelMapper : MapperBase<ArticleTagDto, ArticleTagUpdateViewModel>
{
    public override partial ArticleTagUpdateViewModel Map(ArticleTagDto source);
    public override partial void Map(ArticleTagDto source, ArticleTagUpdateViewModel destination);
}

[Mapper]
public partial class ArticleTagUpdateViewModelToArticleTagUpdateDto : MapperBase<ArticleTagUpdateViewModel, ArticleTagUpdateDto>
{
    public override partial ArticleTagUpdateDto Map(ArticleTagUpdateViewModel source);
    public override partial void Map(ArticleTagUpdateViewModel source, ArticleTagUpdateDto destination);
}

[Mapper]
public partial class ArticleTagCreateViewModelToArticleTagCreateDto : MapperBase<ArticleTagCreateViewModel, ArticleTagCreateDto>
{
    public override partial ArticleTagCreateDto Map(ArticleTagCreateViewModel source);
    public override partial void Map(ArticleTagCreateViewModel source, ArticleTagCreateDto destination);
}

[Mapper]
public partial class ArticleDtoToArticleUpdateViewModelMapper : MapperBase<ArticleDto, ArticleUpdateViewModel>
{
    public override partial ArticleUpdateViewModel Map(ArticleDto source);
    public override partial void Map(ArticleDto source, ArticleUpdateViewModel destination);
}

[Mapper]
public partial class ArticleUpdateViewModelToArticleUpdateDto : MapperBase<ArticleUpdateViewModel, ArticleUpdateDto>
{
    public override partial ArticleUpdateDto Map(ArticleUpdateViewModel source);
    public override partial void Map(ArticleUpdateViewModel source, ArticleUpdateDto destination);
}

[Mapper]
public partial class ArticleCreateViewModelToArticleCreateDto : MapperBase<ArticleCreateViewModel, ArticleCreateDto>
{
    public override partial ArticleCreateDto Map(ArticleCreateViewModel source);
    public override partial void Map(ArticleCreateViewModel source, ArticleCreateDto destination);
}

[Mapper]
public partial class ArticleCreatePageViewModelToArticleCreateDto : MapperBase<ArticleCreatePageViewModel, ArticleCreateDto>
{
    public override partial ArticleCreateDto Map(ArticleCreatePageViewModel source);
    public override partial void Map(ArticleCreatePageViewModel source, ArticleCreateDto destination);
}

[Mapper]
public partial class ArticleDtoToArticleUpdatePageViewModelMapper : MapperBase<ArticleDto, ArticleUpdatePageViewModel>
{
    public override partial ArticleUpdatePageViewModel Map(ArticleDto source);
    public override partial void Map(ArticleDto source, ArticleUpdatePageViewModel destination);
}

[Mapper]
public partial class ArticleUpdatePageViewModelToArticleUpdateDto : MapperBase<ArticleUpdatePageViewModel, ArticleUpdateDto>
{
    public override partial ArticleUpdateDto Map(ArticleUpdatePageViewModel source);
    public override partial void Map(ArticleUpdatePageViewModel source, ArticleUpdateDto destination);
}

[Mapper]
public partial class ArticleTagMappingDtoToArticleTagMappingUpdateViewModelMapper : MapperBase<ArticleTagMappingDto, ArticleTagMappingUpdateViewModel>
{
    public override partial ArticleTagMappingUpdateViewModel Map(ArticleTagMappingDto source);
    public override partial void Map(ArticleTagMappingDto source, ArticleTagMappingUpdateViewModel destination);
}

[Mapper]
public partial class ArticleTagMappingUpdateViewModelToArticleTagMappingUpdateDto : MapperBase<ArticleTagMappingUpdateViewModel, ArticleTagMappingUpdateDto>
{
    public override partial ArticleTagMappingUpdateDto Map(ArticleTagMappingUpdateViewModel source);
    public override partial void Map(ArticleTagMappingUpdateViewModel source, ArticleTagMappingUpdateDto destination);
}

[Mapper]
public partial class ArticleTagMappingCreateViewModelToArticleTagMappingCreateDto : MapperBase<ArticleTagMappingCreateViewModel, ArticleTagMappingCreateDto>
{
    public override partial ArticleTagMappingCreateDto Map(ArticleTagMappingCreateViewModel source);
    public override partial void Map(ArticleTagMappingCreateViewModel source, ArticleTagMappingCreateDto destination);
}

[Mapper]
public partial class ArticleViewDtoToArticleViewUpdateViewModelMapper : MapperBase<ArticleViewDto, ArticleViewUpdateViewModel>
{
    public override partial ArticleViewUpdateViewModel Map(ArticleViewDto source);
    public override partial void Map(ArticleViewDto source, ArticleViewUpdateViewModel destination);
}

[Mapper]
public partial class ArticleViewUpdateViewModelToArticleViewUpdateDto : MapperBase<ArticleViewUpdateViewModel, ArticleViewUpdateDto>
{
    public override partial ArticleViewUpdateDto Map(ArticleViewUpdateViewModel source);
    public override partial void Map(ArticleViewUpdateViewModel source, ArticleViewUpdateDto destination);
}

[Mapper]
public partial class ArticleViewCreateViewModelToArticleViewCreateDto : MapperBase<ArticleViewCreateViewModel, ArticleViewCreateDto>
{
    public override partial ArticleViewCreateDto Map(ArticleViewCreateViewModel source);
    public override partial void Map(ArticleViewCreateViewModel source, ArticleViewCreateDto destination);
}

[Mapper]
public partial class MediaFileDtoToMediaFileUpdateViewModelMapper : MapperBase<MediaFileDto, MediaFileUpdateViewModel>
{
    public override partial MediaFileUpdateViewModel Map(MediaFileDto source);
    public override partial void Map(MediaFileDto source, MediaFileUpdateViewModel destination);
}

[Mapper]
public partial class MediaFileUpdateViewModelToMediaFileUpdateDto : MapperBase<MediaFileUpdateViewModel, MediaFileUpdateDto>
{
    public override partial MediaFileUpdateDto Map(MediaFileUpdateViewModel source);
    public override partial void Map(MediaFileUpdateViewModel source, MediaFileUpdateDto destination);
}

[Mapper]
public partial class MediaFileCreateViewModelToMediaFileCreateDto : MapperBase<MediaFileCreateViewModel, MediaFileCreateDto>
{
    public override partial MediaFileCreateDto Map(MediaFileCreateViewModel source);
    public override partial void Map(MediaFileCreateViewModel source, MediaFileCreateDto destination);
}

[Mapper]
public partial class PlaceCategoryDtoToPlaceCategoryUpdateViewModelMapper : MapperBase<PlaceCategoryDto, PlaceCategoryUpdateViewModel>
{
    public override partial PlaceCategoryUpdateViewModel Map(PlaceCategoryDto source);
    public override partial void Map(PlaceCategoryDto source, PlaceCategoryUpdateViewModel destination);
}

[Mapper]
public partial class PlaceCategoryUpdateViewModelToPlaceCategoryUpdateDto : MapperBase<PlaceCategoryUpdateViewModel, PlaceCategoryUpdateDto>
{
    public override partial PlaceCategoryUpdateDto Map(PlaceCategoryUpdateViewModel source);
    public override partial void Map(PlaceCategoryUpdateViewModel source, PlaceCategoryUpdateDto destination);
}

[Mapper]
public partial class PlaceCategoryCreateViewModelToPlaceCategoryCreateDto : MapperBase<PlaceCategoryCreateViewModel, PlaceCategoryCreateDto>
{
    public override partial PlaceCategoryCreateDto Map(PlaceCategoryCreateViewModel source);
    public override partial void Map(PlaceCategoryCreateViewModel source, PlaceCategoryCreateDto destination);
}

[Mapper]
public partial class PlaceTagDtoToPlaceTagUpdateViewModelMapper : MapperBase<PlaceTagDto, PlaceTagUpdateViewModel>
{
    public override partial PlaceTagUpdateViewModel Map(PlaceTagDto source);
    public override partial void Map(PlaceTagDto source, PlaceTagUpdateViewModel destination);
}

[Mapper]
public partial class PlaceTagUpdateViewModelToPlaceTagUpdateDto : MapperBase<PlaceTagUpdateViewModel, PlaceTagUpdateDto>
{
    public override partial PlaceTagUpdateDto Map(PlaceTagUpdateViewModel source);
    public override partial void Map(PlaceTagUpdateViewModel source, PlaceTagUpdateDto destination);
}

[Mapper]
public partial class PlaceTagCreateViewModelToPlaceTagCreateDto : MapperBase<PlaceTagCreateViewModel, PlaceTagCreateDto>
{
    public override partial PlaceTagCreateDto Map(PlaceTagCreateViewModel source);
    public override partial void Map(PlaceTagCreateViewModel source, PlaceTagCreateDto destination);
}

[Mapper]
public partial class PlaceDtoToPlaceUpdateViewModelMapper : MapperBase<PlaceDto, PlaceUpdateViewModel>
{
    public override partial PlaceUpdateViewModel Map(PlaceDto source);
    public override partial void Map(PlaceDto source, PlaceUpdateViewModel destination);
}

[Mapper]
public partial class PlaceUpdateViewModelToPlaceUpdateDto : MapperBase<PlaceUpdateViewModel, PlaceUpdateDto>
{
    public override partial PlaceUpdateDto Map(PlaceUpdateViewModel source);
    public override partial void Map(PlaceUpdateViewModel source, PlaceUpdateDto destination);
}

[Mapper]
public partial class PlaceCreateViewModelToPlaceCreateDto : MapperBase<PlaceCreateViewModel, PlaceCreateDto>
{
    public override partial PlaceCreateDto Map(PlaceCreateViewModel source);
    public override partial void Map(PlaceCreateViewModel source, PlaceCreateDto destination);
}

[Mapper]
public partial class PlaceTagMappingDtoToPlaceTagMappingUpdateViewModelMapper : MapperBase<PlaceTagMappingDto, PlaceTagMappingUpdateViewModel>
{
    public override partial PlaceTagMappingUpdateViewModel Map(PlaceTagMappingDto source);
    public override partial void Map(PlaceTagMappingDto source, PlaceTagMappingUpdateViewModel destination);
}

[Mapper]
public partial class PlaceTagMappingUpdateViewModelToPlaceTagMappingUpdateDto : MapperBase<PlaceTagMappingUpdateViewModel, PlaceTagMappingUpdateDto>
{
    public override partial PlaceTagMappingUpdateDto Map(PlaceTagMappingUpdateViewModel source);
    public override partial void Map(PlaceTagMappingUpdateViewModel source, PlaceTagMappingUpdateDto destination);
}

[Mapper]
public partial class PlaceTagMappingCreateViewModelToPlaceTagMappingCreateDto : MapperBase<PlaceTagMappingCreateViewModel, PlaceTagMappingCreateDto>
{
    public override partial PlaceTagMappingCreateDto Map(PlaceTagMappingCreateViewModel source);
    public override partial void Map(PlaceTagMappingCreateViewModel source, PlaceTagMappingCreateDto destination);
}

[Mapper]
public partial class EntityFileDtoToEntityFileUpdateViewModelMapper : MapperBase<EntityFileDto, EntityFileUpdateViewModel>
{
    public override partial EntityFileUpdateViewModel Map(EntityFileDto source);
    public override partial void Map(EntityFileDto source, EntityFileUpdateViewModel destination);
}

[Mapper]
public partial class EntityFileUpdateViewModelToEntityFileUpdateDto : MapperBase<EntityFileUpdateViewModel, EntityFileUpdateDto>
{
    public override partial EntityFileUpdateDto Map(EntityFileUpdateViewModel source);
    public override partial void Map(EntityFileUpdateViewModel source, EntityFileUpdateDto destination);
}

[Mapper]
public partial class EntityFileCreateViewModelToEntityFileCreateDto : MapperBase<EntityFileCreateViewModel, EntityFileCreateDto>
{
    public override partial EntityFileCreateDto Map(EntityFileCreateViewModel source);
    public override partial void Map(EntityFileCreateViewModel source, EntityFileCreateDto destination);
}

[Mapper]
public partial class PlaceReviewDtoToPlaceReviewUpdateViewModelMapper : MapperBase<PlaceReviewDto, PlaceReviewUpdateViewModel>
{
    public override partial PlaceReviewUpdateViewModel Map(PlaceReviewDto source);
    public override partial void Map(PlaceReviewDto source, PlaceReviewUpdateViewModel destination);
}

[Mapper]
public partial class PlaceReviewUpdateViewModelToPlaceReviewUpdateDto : MapperBase<PlaceReviewUpdateViewModel, PlaceReviewUpdateDto>
{
    public override partial PlaceReviewUpdateDto Map(PlaceReviewUpdateViewModel source);
    public override partial void Map(PlaceReviewUpdateViewModel source, PlaceReviewUpdateDto destination);
}

[Mapper]
public partial class PlaceReviewCreateViewModelToPlaceReviewCreateDto : MapperBase<PlaceReviewCreateViewModel, PlaceReviewCreateDto>
{
    public override partial PlaceReviewCreateDto Map(PlaceReviewCreateViewModel source);
    public override partial void Map(PlaceReviewCreateViewModel source, PlaceReviewCreateDto destination);
}

[Mapper]
public partial class PlaceFavoriteDtoToPlaceFavoriteUpdateViewModelMapper : MapperBase<PlaceFavoriteDto, PlaceFavoriteUpdateViewModel>
{
    public override partial PlaceFavoriteUpdateViewModel Map(PlaceFavoriteDto source);
    public override partial void Map(PlaceFavoriteDto source, PlaceFavoriteUpdateViewModel destination);
}

[Mapper]
public partial class PlaceFavoriteUpdateViewModelToPlaceFavoriteUpdateDto : MapperBase<PlaceFavoriteUpdateViewModel, PlaceFavoriteUpdateDto>
{
    public override partial PlaceFavoriteUpdateDto Map(PlaceFavoriteUpdateViewModel source);
    public override partial void Map(PlaceFavoriteUpdateViewModel source, PlaceFavoriteUpdateDto destination);
}

[Mapper]
public partial class PlaceFavoriteCreateViewModelToPlaceFavoriteCreateDto : MapperBase<PlaceFavoriteCreateViewModel, PlaceFavoriteCreateDto>
{
    public override partial PlaceFavoriteCreateDto Map(PlaceFavoriteCreateViewModel source);
    public override partial void Map(PlaceFavoriteCreateViewModel source, PlaceFavoriteCreateDto destination);
}

[Mapper]
public partial class PlaceViewDtoToPlaceViewUpdateViewModelMapper : MapperBase<PlaceViewDto, PlaceViewUpdateViewModel>
{
    public override partial PlaceViewUpdateViewModel Map(PlaceViewDto source);
    public override partial void Map(PlaceViewDto source, PlaceViewUpdateViewModel destination);
}

[Mapper]
public partial class PlaceViewUpdateViewModelToPlaceViewUpdateDto : MapperBase<PlaceViewUpdateViewModel, PlaceViewUpdateDto>
{
    public override partial PlaceViewUpdateDto Map(PlaceViewUpdateViewModel source);
    public override partial void Map(PlaceViewUpdateViewModel source, PlaceViewUpdateDto destination);
}

[Mapper]
public partial class PlaceViewCreateViewModelToPlaceViewCreateDto : MapperBase<PlaceViewCreateViewModel, PlaceViewCreateDto>
{
    public override partial PlaceViewCreateDto Map(PlaceViewCreateViewModel source);
    public override partial void Map(PlaceViewCreateViewModel source, PlaceViewCreateDto destination);
}