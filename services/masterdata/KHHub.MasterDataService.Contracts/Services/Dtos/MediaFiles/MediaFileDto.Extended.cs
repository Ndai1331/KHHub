namespace KHHub.MasterDataService.Services.Dtos.MediaFiles;

public class MediaFileDto : MediaFileDtoBase
{
    /// <summary>
    /// Relative URL safe to persist (no presigned query string), e.g. /khhub-articles/host/file.png.
    /// Derived from <see cref="MediaFilesStorageOptions.PublicBaseUrl"/> and blob <see cref="MediaFileDtoBase.Path"/>.
    /// </summary>
    public string? PublicPath { get; set; }
}