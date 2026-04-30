using Volo.Abp.Reflection;

namespace KHHub.MasterDataService.Permissions;

public class MasterDataServicePermissions
{
    public const string GroupName = "MasterDataService";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(MasterDataServicePermissions));
    }

    public static class Provinces
    {
        public const string Default = GroupName + ".Provinces";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Wards
    {
        public const string Default = GroupName + ".Wards";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}