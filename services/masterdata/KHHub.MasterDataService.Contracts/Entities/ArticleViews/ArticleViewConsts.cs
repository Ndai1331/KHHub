namespace KHHub.MasterDataService.Entities.ArticleViews;

public static class ArticleViewConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "ArticleView." : string.Empty);
    }

    public const int IpAddressMaxLength = 20;
    public const int DeviceMaxLength = 100;
    public const int SourceMaxLength = 255;
}