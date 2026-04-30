namespace KHHub.MasterDataService.Entities.Articles;

public static class ArticleConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "Article." : string.Empty);
    }

    public const int TitleMaxLength = 255;
    public const int SlugMaxLength = 255;
    public const int SummaryMaxLength = 500;
    public const int ThumbnailUrlMaxLength = 255;
    public const int CoverImageUrlMaxLength = 255;
    public const int AuthorNameMaxLength = 100;
    public const int SourceMaxLength = 255;
    public const int SourceUrlMaxLength = 255;
    public const int SeoTitleMaxLength = 255;
}