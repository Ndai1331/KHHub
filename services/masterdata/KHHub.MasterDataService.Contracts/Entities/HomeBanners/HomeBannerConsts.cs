namespace KHHub.MasterDataService.Entities.HomeBanners;

public static class HomeBannerConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "HomeBanner." : string.Empty);
    }

    public const int TitleMaxLength = 255;
    public const int SubtitleMaxLength = 500;
    public const int ImageUrlMaxLength = 1000;
    public const int MobileImageUrlMaxLength = 1000;
    public const int ButtonTextMaxLength = 100;
    public const int ButtonUrlMaxLength = 1000;
}