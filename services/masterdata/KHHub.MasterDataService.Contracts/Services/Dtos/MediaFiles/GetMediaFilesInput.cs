using KHHub.MasterDataService.Entities.MediaFiles;
using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.MediaFiles;

public abstract class GetMediaFilesInputBase : PagedAndSortedResultRequestDto
{
    public string? FilterText { get; set; }

    public string? FileName { get; set; }

    public string? OriginalFileName { get; set; }

    public string? Extension { get; set; }

    public string? ContentType { get; set; }

    public string? StorageProvider { get; set; }

    public string? Bucket { get; set; }

    public string? Folder { get; set; }

    public string? Path { get; set; }

    public string? Url { get; set; }

    public string? Checksum { get; set; }

    public int? WidthMin { get; set; }

    public int? WidthMax { get; set; }

    public int? HeightMin { get; set; }

    public int? HeightMax { get; set; }

    public FileType? FileType { get; set; }

    public FileStatus? Status { get; set; }

    public GetMediaFilesInputBase()
    {
    }
}