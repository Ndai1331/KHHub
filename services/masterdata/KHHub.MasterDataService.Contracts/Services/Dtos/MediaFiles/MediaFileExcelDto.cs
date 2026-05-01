using KHHub.MasterDataService.Entities.MediaFiles;
using System;

namespace KHHub.MasterDataService.Services.Dtos.MediaFiles;

public abstract class MediaFileExcelDtoBase
{
    public string? FileName { get; set; }

    public string? OriginalFileName { get; set; }

    public string? Extension { get; set; }

    public string? ContentType { get; set; }

    public long Size { get; set; }

    public string? StorageProvider { get; set; }

    public string? Bucket { get; set; }

    public string? Folder { get; set; }

    public string? Path { get; set; }

    public string? Url { get; set; }

    public string? Checksum { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public int Duration { get; set; }

    public FileType FileType { get; set; }

    public FileStatus Status { get; set; }
}