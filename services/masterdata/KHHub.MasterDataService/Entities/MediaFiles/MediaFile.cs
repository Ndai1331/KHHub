using KHHub.MasterDataService.Entities.MediaFiles;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.MediaFiles;

public abstract class MediaFileBase : FullAuditedAggregateRoot<Guid>
{
    [CanBeNull]
    public virtual string? FileName { get; set; }

    [CanBeNull]
    public virtual string? OriginalFileName { get; set; }

    [CanBeNull]
    public virtual string? Extension { get; set; }

    [CanBeNull]
    public virtual string? ContentType { get; set; }

    public virtual long Size { get; set; }

    [CanBeNull]
    public virtual string? StorageProvider { get; set; }

    [CanBeNull]
    public virtual string? Bucket { get; set; }

    [CanBeNull]
    public virtual string? Folder { get; set; }

    [CanBeNull]
    public virtual string? Path { get; set; }

    [CanBeNull]
    public virtual string? Url { get; set; }

    [CanBeNull]
    public virtual string? Checksum { get; set; }

    public virtual int Width { get; set; }

    public virtual int Height { get; set; }

    public virtual int Duration { get; set; }

    public virtual FileType FileType { get; set; }

    public virtual FileStatus Status { get; set; }

    protected MediaFileBase()
    {
    }

    public MediaFileBase(Guid id, long size, int width, int height, int duration, FileType fileType, FileStatus status, string? fileName = null, string? originalFileName = null, string? extension = null, string? contentType = null, string? storageProvider = null, string? bucket = null, string? folder = null, string? path = null, string? url = null, string? checksum = null)
    {
        Id = id;
        Check.Length(fileName, nameof(fileName), MediaFileConsts.FileNameMaxLength, 0);
        Check.Length(originalFileName, nameof(originalFileName), MediaFileConsts.OriginalFileNameMaxLength, 0);
        Check.Length(extension, nameof(extension), MediaFileConsts.ExtensionMaxLength, 0);
        Check.Length(contentType, nameof(contentType), MediaFileConsts.ContentTypeMaxLength, 0);
        Check.Length(storageProvider, nameof(storageProvider), MediaFileConsts.StorageProviderMaxLength, 0);
        Check.Length(bucket, nameof(bucket), MediaFileConsts.BucketMaxLength, 0);
        Check.Length(folder, nameof(folder), MediaFileConsts.FolderMaxLength, 0);
        Check.Length(path, nameof(path), MediaFileConsts.PathMaxLength, 0);
        Check.Length(url, nameof(url), MediaFileConsts.UrlMaxLength, 0);
        Check.Length(checksum, nameof(checksum), MediaFileConsts.ChecksumMaxLength, 0);
        Size = size;
        Width = width;
        Height = height;
        Duration = duration;
        FileType = fileType;
        Status = status;
        FileName = fileName;
        OriginalFileName = originalFileName;
        Extension = extension;
        ContentType = contentType;
        StorageProvider = storageProvider;
        Bucket = bucket;
        Folder = folder;
        Path = path;
        Url = url;
        Checksum = checksum;
    }
}