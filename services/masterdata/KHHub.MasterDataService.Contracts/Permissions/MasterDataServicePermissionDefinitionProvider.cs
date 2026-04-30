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
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MasterDataServiceResource>(name);
    }
}