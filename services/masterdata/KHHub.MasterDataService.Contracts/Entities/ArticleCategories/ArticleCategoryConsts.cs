namespace KHHub.MasterDataService.Entities.ArticleCategories;

public static class ArticleCategoryConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "ArticleCategory." : string.Empty);
    }

    public const int NameMaxLength = 255;
    public const int SlugMaxLength = 255;
    public const int IconMaxLength = 20;
    public const int ThumbnailUrlMaxLength = 255;
}