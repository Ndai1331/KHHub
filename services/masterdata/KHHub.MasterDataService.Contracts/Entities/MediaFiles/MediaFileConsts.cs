namespace KHHub.MasterDataService.Entities.MediaFiles;

public static class MediaFileConsts
{
    private const string DefaultSorting = "{0}CreationTime desc";

    public static string GetDefaultSorting(bool withEntityName)
    {
        return string.Format(DefaultSorting, withEntityName ? "MediaFile." : string.Empty);
    }

    public const int FileNameMaxLength = 255;
    public const int OriginalFileNameMaxLength = 255;
    public const int ExtensionMaxLength = 20;
    public const int ContentTypeMaxLength = 100;
    public const int StorageProviderMaxLength = 50;
    public const int BucketMaxLength = 100;
    public const int FolderMaxLength = 255;
    public const int PathMaxLength = 500;
    public const int UrlMaxLength = 1000;
    public const int ChecksumMaxLength = 128;
}