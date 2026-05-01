using KHHub.MasterDataService.Entities.MediaFiles;
using KHHub.MasterDataService.Entities.MediaFiles;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.MediaFiles;

public abstract class MediaFileUpdateDtoBase : IHasConcurrencyStamp
{
    [StringLength(MediaFileConsts.FileNameMaxLength)]
    public string? FileName { get; set; }

    [StringLength(MediaFileConsts.OriginalFileNameMaxLength)]
    public string? OriginalFileName { get; set; }

    [StringLength(MediaFileConsts.ExtensionMaxLength)]
    public string? Extension { get; set; }

    [StringLength(MediaFileConsts.ContentTypeMaxLength)]
    public string? ContentType { get; set; }

    public long Size { get; set; }

    [StringLength(MediaFileConsts.StorageProviderMaxLength)]
    public string? StorageProvider { get; set; }

    [StringLength(MediaFileConsts.BucketMaxLength)]
    public string? Bucket { get; set; }

    [StringLength(MediaFileConsts.FolderMaxLength)]
    public string? Folder { get; set; }

    [StringLength(MediaFileConsts.PathMaxLength)]
    public string? Path { get; set; }

    [StringLength(MediaFileConsts.UrlMaxLength)]
    public string? Url { get; set; }

    [StringLength(MediaFileConsts.ChecksumMaxLength)]
    public string? Checksum { get; set; }

    public int Width { get; set; }

    public int Height { get; set; }

    public int Duration { get; set; }

    public FileType FileType { get; set; }

    public FileStatus Status { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}