namespace KHHub.MasterDataService.Entities.PlaceTagMappings;

public static class PlaceTagMappingConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "PlaceTagMapping." : string.Empty);
    }
}