namespace KHHub.MasterDataService.Entities.Jobs;

public static class JobConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "Job." : string.Empty);
    }

    public const int TitleMaxLength = 255;
    public const int SlugMaxLength = 300;
    public const int SummaryMaxLength = 500;
    public const int ThumbnailUrlMaxLength = 1000;
    public const int CoverImageUrlMaxLength = 1000;
    public const int SalaryTextMaxLength = 150;
    public const int SalaryCurrencyMaxLength = 20;
    public const int LocationMaxLength = 255;
    public const int ContactEmailMaxLength = 255;
    public const int ContactPhoneMaxLength = 20;
    public const int ApplicationUrlMaxLength = 1000;
    public const int SeoTitleMaxLength = 255;
    public const int SeoDescriptionMaxLength = 500;
    public const int SeoKeywordsMaxLength = 500;
}