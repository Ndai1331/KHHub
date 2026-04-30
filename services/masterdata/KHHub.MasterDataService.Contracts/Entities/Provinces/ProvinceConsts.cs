namespace KHHub.MasterDataService.Entities.Provinces;

public static class ProvinceConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "Province." : string.Empty);
    }

    public const int CodeMaxLength = 12;
    public const int NameMaxLength = 255;
}