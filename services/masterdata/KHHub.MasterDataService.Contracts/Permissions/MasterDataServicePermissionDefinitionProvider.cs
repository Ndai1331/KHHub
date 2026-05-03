using KHHub.MasterDataService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace KHHub.MasterDataService.Permissions;

public class MasterDataServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(MasterDataServicePermissions.GroupName);
        var provincePermission = myGroup.AddPermission(MasterDataServicePermissions.Provinces.Default, L("Permission:Provinces"));
        provincePermission.AddChild(MasterDataServicePermissions.Provinces.Create, L("Permission:Create"));
        provincePermission.AddChild(MasterDataServicePermissions.Provinces.Edit, L("Permission:Edit"));
        provincePermission.AddChild(MasterDataServicePermissions.Provinces.Delete, L("Permission:Delete"));
        var wardPermission = myGroup.AddPermission(MasterDataServicePermissions.Wards.Default, L("Permission:Wards"));
        wardPermission.AddChild(MasterDataServicePermissions.Wards.Create, L("Permission:Create"));
        wardPermission.AddChild(MasterDataServicePermissions.Wards.Edit, L("Permission:Edit"));
        wardPermission.AddChild(MasterDataServicePermissions.Wards.Delete, L("Permission:Delete"));
        var articleCategoryPermission = myGroup.AddPermission(MasterDataServicePermissions.ArticleCategories.Default, L("Permission:ArticleCategories"));
        articleCategoryPermission.AddChild(MasterDataServicePermissions.ArticleCategories.Create, L("Permission:Create"));
        articleCategoryPermission.AddChild(MasterDataServicePermissions.ArticleCategories.Edit, L("Permission:Edit"));
        articleCategoryPermission.AddChild(MasterDataServicePermissions.ArticleCategories.Delete, L("Permission:Delete"));
        var articleTagPermission = myGroup.AddPermission(MasterDataServicePermissions.ArticleTags.Default, L("Permission:ArticleTags"));
        articleTagPermission.AddChild(MasterDataServicePermissions.ArticleTags.Create, L("Permission:Create"));
        articleTagPermission.AddChild(MasterDataServicePermissions.ArticleTags.Edit, L("Permission:Edit"));
        articleTagPermission.AddChild(MasterDataServicePermissions.ArticleTags.Delete, L("Permission:Delete"));
        var articlePermission = myGroup.AddPermission(MasterDataServicePermissions.Articles.Default, L("Permission:Articles"));
        articlePermission.AddChild(MasterDataServicePermissions.Articles.Create, L("Permission:Create"));
        articlePermission.AddChild(MasterDataServicePermissions.Articles.Edit, L("Permission:Edit"));
        articlePermission.AddChild(MasterDataServicePermissions.Articles.Delete, L("Permission:Delete"));
        var articleTagMappingPermission = myGroup.AddPermission(MasterDataServicePermissions.ArticleTagMappings.Default, L("Permission:ArticleTagMappings"));
        articleTagMappingPermission.AddChild(MasterDataServicePermissions.ArticleTagMappings.Create, L("Permission:Create"));
        articleTagMappingPermission.AddChild(MasterDataServicePermissions.ArticleTagMappings.Edit, L("Permission:Edit"));
        articleTagMappingPermission.AddChild(MasterDataServicePermissions.ArticleTagMappings.Delete, L("Permission:Delete"));
        var articleViewPermission = myGroup.AddPermission(MasterDataServicePermissions.ArticleViews.Default, L("Permission:ArticleViews"));
        articleViewPermission.AddChild(MasterDataServicePermissions.ArticleViews.Create, L("Permission:Create"));
        articleViewPermission.AddChild(MasterDataServicePermissions.ArticleViews.Edit, L("Permission:Edit"));
        articleViewPermission.AddChild(MasterDataServicePermissions.ArticleViews.Delete, L("Permission:Delete"));
        var mediaFilePermission = myGroup.AddPermission(MasterDataServicePermissions.MediaFiles.Default, L("Permission:MediaFiles"));
        mediaFilePermission.AddChild(MasterDataServicePermissions.MediaFiles.Create, L("Permission:Create"));
        mediaFilePermission.AddChild(MasterDataServicePermissions.MediaFiles.Edit, L("Permission:Edit"));
        mediaFilePermission.AddChild(MasterDataServicePermissions.MediaFiles.Delete, L("Permission:Delete"));
        var placeCategoryPermission = myGroup.AddPermission(MasterDataServicePermissions.PlaceCategories.Default, L("Permission:PlaceCategories"));
        placeCategoryPermission.AddChild(MasterDataServicePermissions.PlaceCategories.Create, L("Permission:Create"));
        placeCategoryPermission.AddChild(MasterDataServicePermissions.PlaceCategories.Edit, L("Permission:Edit"));
        placeCategoryPermission.AddChild(MasterDataServicePermissions.PlaceCategories.Delete, L("Permission:Delete"));
        var placeTagPermission = myGroup.AddPermission(MasterDataServicePermissions.PlaceTags.Default, L("Permission:PlaceTags"));
        placeTagPermission.AddChild(MasterDataServicePermissions.PlaceTags.Create, L("Permission:Create"));
        placeTagPermission.AddChild(MasterDataServicePermissions.PlaceTags.Edit, L("Permission:Edit"));
        placeTagPermission.AddChild(MasterDataServicePermissions.PlaceTags.Delete, L("Permission:Delete"));
        var placePermission = myGroup.AddPermission(MasterDataServicePermissions.Places.Default, L("Permission:Places"));
        placePermission.AddChild(MasterDataServicePermissions.Places.Create, L("Permission:Create"));
        placePermission.AddChild(MasterDataServicePermissions.Places.Edit, L("Permission:Edit"));
        placePermission.AddChild(MasterDataServicePermissions.Places.Delete, L("Permission:Delete"));
        var placeTagMappingPermission = myGroup.AddPermission(MasterDataServicePermissions.PlaceTagMappings.Default, L("Permission:PlaceTagMappings"));
        placeTagMappingPermission.AddChild(MasterDataServicePermissions.PlaceTagMappings.Create, L("Permission:Create"));
        placeTagMappingPermission.AddChild(MasterDataServicePermissions.PlaceTagMappings.Edit, L("Permission:Edit"));
        placeTagMappingPermission.AddChild(MasterDataServicePermissions.PlaceTagMappings.Delete, L("Permission:Delete"));
        var entityFilePermission = myGroup.AddPermission(MasterDataServicePermissions.EntityFiles.Default, L("Permission:EntityFiles"));
        entityFilePermission.AddChild(MasterDataServicePermissions.EntityFiles.Create, L("Permission:Create"));
        entityFilePermission.AddChild(MasterDataServicePermissions.EntityFiles.Edit, L("Permission:Edit"));
        entityFilePermission.AddChild(MasterDataServicePermissions.EntityFiles.Delete, L("Permission:Delete"));
        var placeReviewPermission = myGroup.AddPermission(MasterDataServicePermissions.PlaceReviews.Default, L("Permission:PlaceReviews"));
        placeReviewPermission.AddChild(MasterDataServicePermissions.PlaceReviews.Create, L("Permission:Create"));
        placeReviewPermission.AddChild(MasterDataServicePermissions.PlaceReviews.Edit, L("Permission:Edit"));
        placeReviewPermission.AddChild(MasterDataServicePermissions.PlaceReviews.Delete, L("Permission:Delete"));
        var placeFavoritePermission = myGroup.AddPermission(MasterDataServicePermissions.PlaceFavorites.Default, L("Permission:PlaceFavorites"));
        placeFavoritePermission.AddChild(MasterDataServicePermissions.PlaceFavorites.Create, L("Permission:Create"));
        placeFavoritePermission.AddChild(MasterDataServicePermissions.PlaceFavorites.Edit, L("Permission:Edit"));
        placeFavoritePermission.AddChild(MasterDataServicePermissions.PlaceFavorites.Delete, L("Permission:Delete"));
        var placeViewPermission = myGroup.AddPermission(MasterDataServicePermissions.PlaceViews.Default, L("Permission:PlaceViews"));
        placeViewPermission.AddChild(MasterDataServicePermissions.PlaceViews.Create, L("Permission:Create"));
        placeViewPermission.AddChild(MasterDataServicePermissions.PlaceViews.Edit, L("Permission:Edit"));
        placeViewPermission.AddChild(MasterDataServicePermissions.PlaceViews.Delete, L("Permission:Delete"));
        var homeBannerPermission = myGroup.AddPermission(MasterDataServicePermissions.HomeBanners.Default, L("Permission:HomeBanners"));
        homeBannerPermission.AddChild(MasterDataServicePermissions.HomeBanners.Create, L("Permission:Create"));
        homeBannerPermission.AddChild(MasterDataServicePermissions.HomeBanners.Edit, L("Permission:Edit"));
        homeBannerPermission.AddChild(MasterDataServicePermissions.HomeBanners.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MasterDataServiceResource>(name);
    }
}