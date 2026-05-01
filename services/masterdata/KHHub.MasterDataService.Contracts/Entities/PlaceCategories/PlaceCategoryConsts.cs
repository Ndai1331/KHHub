namespace KHHub.MasterDataService.Entities.PlaceCategories;

public static class PlaceCategoryConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "PlaceCategory." : string.Empty);
    }

    public const int NameMaxLength = 150;
    public const int SlugMaxLength = 200;
    public const int DescriptionMaxLength = 500;
    public const int IconMaxLength = 50;
    public const int ColorMaxLength = 20;
}