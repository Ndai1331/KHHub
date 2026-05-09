namespace KHHub.MasterDataService.Entities.JobTagMappings;

public static class JobTagMappingConsts
{
    private const string DefaultSorting = "{0}SortOrder asc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "JobTagMapping." : string.Empty);
    }
}