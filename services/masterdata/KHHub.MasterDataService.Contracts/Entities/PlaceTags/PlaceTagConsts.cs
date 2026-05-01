namespace KHHub.MasterDataService.Entities.PlaceTags;

public static class PlaceTagConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "PlaceTag." : string.Empty);
    }

    public const int NameMaxLength = 100;
    public const int SlugMaxLength = 150;
    public const int DescriptionMaxLength = 500;
}