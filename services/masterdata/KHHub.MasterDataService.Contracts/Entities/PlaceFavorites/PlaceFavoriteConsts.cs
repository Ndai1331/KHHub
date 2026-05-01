namespace KHHub.MasterDataService.Entities.PlaceFavorites;

public static class PlaceFavoriteConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "PlaceFavorite." : string.Empty);
    }
}