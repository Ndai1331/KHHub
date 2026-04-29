using KHHub.GdprService.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace KHHub.GdprService.Permissions;

public class GdprServicePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //var myGroup = context.AddGroup(GdprServicePermissions.GroupName);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<GdprServiceResource>(name);
    }
}