namespace KHHub.MasterDataService.Entities.ArticleTags;

public static class ArticleTagConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "ArticleTag." : string.Empty);
    }

    public const int NameMaxLength = 255;
    public const int SlugMaxLength = 255;
}