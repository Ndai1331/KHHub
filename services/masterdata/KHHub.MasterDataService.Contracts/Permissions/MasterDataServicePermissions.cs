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

    public static class PlaceCategories
    {
        public const string Default = GroupName + ".PlaceCategories";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PlaceTags
    {
        public const string Default = GroupName + ".PlaceTags";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class Places
    {
        public const string Default = GroupName + ".Places";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PlaceTagMappings
    {
        public const string Default = GroupName + ".PlaceTagMappings";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class EntityFiles
    {
        public const string Default = GroupName + ".EntityFiles";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PlaceReviews
    {
        public const string Default = GroupName + ".PlaceReviews";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PlaceFavorites
    {
        public const string Default = GroupName + ".PlaceFavorites";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class PlaceViews
    {
        public const string Default = GroupName + ".PlaceViews";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class HomeBanners
    {
        public const string Default = GroupName + ".HomeBanners";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}