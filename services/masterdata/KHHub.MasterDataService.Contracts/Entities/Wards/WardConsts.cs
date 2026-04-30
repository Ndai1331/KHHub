namespace KHHub.MasterDataService.Entities.Wards;

public static class WardConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "Ward." : string.Empty);
    }

    public const int CodeMaxLength = 10;
    public const int NameMaxLength = 255;
}