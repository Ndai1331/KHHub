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