using Volo.Abp.Reflection;

namespace KHHub.CrawlerSerivce.Permissions;

public class CrawlerSerivcePermissions
{
    public const string GroupName = "CrawlerSerivce";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(CrawlerSerivcePermissions));
    }
}