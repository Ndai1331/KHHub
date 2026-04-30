namespace KHHub.MasterDataService.Entities.ArticleTagMappings;

public static class ArticleTagMappingConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "ArticleTagMapping." : string.Empty);
    }
}