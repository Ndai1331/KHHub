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
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MasterDataServiceResource>(name);
    }
}