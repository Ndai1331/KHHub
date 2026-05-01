using KHHub.MasterDataService.Entities.MediaFiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.MediaFiles;

namespace KHHub.MasterDataService.Entities.MediaFiles;

public abstract class MediaFileManagerBase : DomainService
{
    protected IMediaFileRepository _mediaFileRepository;

    public MediaFileManagerBase(IMediaFileRepository mediaFileRepository)
    {
        _mediaFileRepository = mediaFileRepository;
    }

    public virtual async Task<MediaFile> CreateAsync(long size, int width, int height, int duration, FileType fileType, FileStatus status, string? fileName = null, string? originalFileName = null, string? extension = null, string? contentType = null, string? storageProvider = null, string? bucket = null, string? folder = null, string? path = null, string? url = null, string? checksum = null)
    {
        Check.NotNull(fileType, nameof(fileType));
        Check.NotNull(status, nameof(status));
        Check.Length(fileName, nameof(fileName), MediaFileConsts.FileNameMaxLength);
        Check.Length(originalFileName, nameof(originalFileName), MediaFileConsts.OriginalFileNameMaxLength);
        Check.Length(extension, nameof(extension), MediaFileConsts.ExtensionMaxLength);
        Check.Length(contentType, nameof(contentType), MediaFileConsts.ContentTypeMaxLength);
        Check.Length(storageProvider, nameof(storageProvider), MediaFileConsts.StorageProviderMaxLength);
        Check.Length(bucket, nameof(bucket), MediaFileConsts.BucketMaxLength);
        Check.Length(folder, nameof(folder), MediaFileConsts.FolderMaxLength);
        Check.Length(path, nameof(path), MediaFileConsts.PathMaxLength);
        Check.Length(url, nameof(url), MediaFileConsts.UrlMaxLength);
        Check.Length(checksum, nameof(checksum), MediaFileConsts.ChecksumMaxLength);
        var mediaFile = new MediaFile(GuidGenerator.Create(), size, width, height, duration, fileType, status, fileName, originalFileName, extension, contentType, storageProvider, bucket, folder, path, url, checksum);
        return await _mediaFileRepository.InsertAsync(mediaFile);
    }

    public virtual async Task<MediaFile> UpdateAsync(Guid id, long size, int width, int height, int duration, FileType fileType, FileStatus status, string? fileName = null, string? originalFileName = null, string? extension = null, string? contentType = null, string? storageProvider = null, string? bucket = null, string? folder = null, string? path = null, string? url = null, string? checksum = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(fileType, nameof(fileType));
        Check.NotNull(status, nameof(status));
        Check.Length(fileName, nameof(fileName), MediaFileConsts.FileNameMaxLength);
        Check.Length(originalFileName, nameof(originalFileName), MediaFileConsts.OriginalFileNameMaxLength);
        Check.Length(extension, nameof(extension), MediaFileConsts.ExtensionMaxLength);
        Check.Length(contentType, nameof(contentType), MediaFileConsts.ContentTypeMaxLength);
        Check.Length(storageProvider, nameof(storageProvider), MediaFileConsts.StorageProviderMaxLength);
        Check.Length(bucket, nameof(bucket), MediaFileConsts.BucketMaxLength);
        Check.Length(folder, nameof(folder), MediaFileConsts.FolderMaxLength);
        Check.Length(path, nameof(path), MediaFileConsts.PathMaxLength);
        Check.Length(url, nameof(url), MediaFileConsts.UrlMaxLength);
        Check.Length(checksum, nameof(checksum), MediaFileConsts.ChecksumMaxLength);
        var mediaFile = await _mediaFileRepository.GetAsync(id);
        mediaFile.Size = size;
        mediaFile.Width = width;
        mediaFile.Height = height;
        mediaFile.Duration = duration;
        mediaFile.FileType = fileType;
        mediaFile.Status = status;
        mediaFile.FileName = fileName;
        mediaFile.OriginalFileName = originalFileName;
        mediaFile.Extension = extension;
        mediaFile.ContentType = contentType;
        mediaFile.StorageProvider = storageProvider;
        mediaFile.Bucket = bucket;
        mediaFile.Folder = folder;
        mediaFile.Path = path;
        mediaFile.Url = url;
        mediaFile.Checksum = checksum;
        mediaFile.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _mediaFileRepository.UpdateAsync(mediaFile);
    }
}