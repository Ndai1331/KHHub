namespace KHHub.MasterDataService.Entities.Places;

public static class PlaceConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "Place." : string.Empty);
    }

    public const int NameMaxLength = 255;
    public const int SlugMaxLength = 300;
    public const int ShortDescriptionMaxLength = 500;
    public const int ThumbnailUrlMaxLength = 1000;
    public const int CoverImageUrlMaxLength = 1000;
    public const int AddressMaxLength = 500;
    public const int PhoneNumberMaxLength = 50;
    public const int EmailMaxLength = 255;
    public const int WebsiteMaxLength = 500;
    public const int OpeningHoursMaxLength = 500;
    public const int GoogleMapUrlMaxLength = 1000;
    public const int SeoTitleMaxLength = 255;
    public const int SeoDescriptionMaxLength = 500;
    public const int SeoKeywordsMaxLength = 500;
}