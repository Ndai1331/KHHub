namespace KHHub.MasterDataService.Entities.JobCategories;

public static class JobCategoryConsts
{
    private const string DefaultSorting = "{0}DisplayOrder asc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "JobCategory." : string.Empty);
    }

    public const int NameMaxLength = 150;
    public const int SlugMaxLength = 200;
    public const int DescriptionMaxLength = 500;
    public const int IconMaxLength = 50;
    public const int ColorMaxLength = 20;
}