using KHHub.MasterDataService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace KHHub.MasterDataService.Permissions;

public class MasterDataServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //var myGroup = context.AddGroup(MasterDataServicePermissions.GroupName);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<MasterDataServiceResource>(name);
    }
}