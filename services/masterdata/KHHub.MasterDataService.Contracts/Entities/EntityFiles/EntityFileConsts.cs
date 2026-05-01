namespace KHHub.MasterDataService.Entities.EntityFiles;

public static class EntityFileConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "EntityFile." : string.Empty);
    }

    public const int EntityTypeMaxLength = 100;
    public const int CollectionMaxLength = 100;
}