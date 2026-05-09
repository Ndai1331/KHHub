using KHHub.CrawlerSerivce.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace KHHub.CrawlerSerivce.Permissions;

public class CrawlerSerivcePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        //var myGroup = context.AddGroup(CrawlerSerivcePermissions.GroupName);
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<CrawlerSerivceResource>(name);
    }
}