namespace KHHub.MasterDataService.Entities.JobViews;

public static class JobViewConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "JobView." : string.Empty);
    }

    public const int IpAddressMaxLength = 100;
    public const int DeviceMaxLength = 255;
    public const int SourceMaxLength = 100;
}