namespace KHHub.MasterDataService.Entities.JobFavorites;

public static class JobFavoriteConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "JobFavorite." : string.Empty);
    }
}