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

    public static class ArticleCategories
    {
        public const string Default = GroupName + ".ArticleCategories";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class ArticleTags
    {
        public const string Default = GroupName + ".ArticleTags";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Articles
    {
        public const string Default = GroupName + ".Articles";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class ArticleTagMappings
    {
        public const string Default = GroupName + ".ArticleTagMappings";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class ArticleViews
    {
        public const string Default = GroupName + ".ArticleViews";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class MediaFiles
    {
        public const string Default = GroupName + ".MediaFiles";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}