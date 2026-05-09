namespace KHHub.MasterDataService.Entities.JobTags;

public static class JobTagConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "JobTag." : string.Empty);
    }

    public const int NameMaxLength = 100;
    public const int SlugMaxLength = 150;
    public const int DescriptionMaxLength = 500;
}