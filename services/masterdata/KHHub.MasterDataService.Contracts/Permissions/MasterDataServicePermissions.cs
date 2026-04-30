using Volo.Abp.Reflection;

namespace KHHub.MasterDataService.Permissions;

public class MasterDataServicePermissions
{
    public const string GroupName = "MasterDataService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(MasterDataServicePermissions));
    }
}