using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.BlobContainers;
using KHHub.MasterDataService.Data;
using KHHub.MasterDataService.Data.MediaFiles;
using KHHub.MasterDataService.Entities.MediaFiles;
using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService.Services.Dtos.MediaFiles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.BlobStoring;
using Volo.Abp.Caching;
using Volo.Abp.Content;
using Microsoft.Extensions.Caching.Distributed;

namespace KHHub.MasterDataService.Services.MediaFiles;

public class MediaFilesAppService : MediaFilesAppServiceBase, IMediaFilesAppService
{
    private readonly IBlobContainer<MediaFilesBlobContainer> _mediaFilesBlobContainer;
    private readonly IConfiguration _configuration;
    private readonly IOptionsSnapshot<MediaFilesStorageOptions> _mediaStorageOptions;
    private readonly IMinioBlobReadUrlSigner _minioBlobReadUrlSigner;

    public MediaFilesAppService(
        IMediaFileRepository mediaFileRepository,
        MediaFileManager mediaFileManager,
        IDistributedCache<MediaFileDownloadTokenCacheItem, string> downloadTokenCache,
        IBlobContainer<MediaFilesBlobContainer> mediaFilesBlobContainer,
        IConfiguration configuration,
        IOptionsSnapshot<MediaFilesStorageOptions> mediaStorageOptions,
        IMinioBlobReadUrlSigner minioBlobReadUrlSigner)
        : base(mediaFileRepository, mediaFileManager, downloadTokenCache)
    {
        _mediaFilesBlobContainer = mediaFilesBlobContainer;
        _configuration = configuration;
        _mediaStorageOptions = mediaStorageOptions;
        _minioBlobReadUrlSigner = minioBlobReadUrlSigner;
    }

    public virtual async Task<PagedResultDto<MediaFileDto>> GetExplorerListAsync(GetMediaFilesExplorerInput input)
    {
        var parentNorm = MediaExplorerPathHelper.NormalizeParentFolderPath(input.ParentFolderPath);
        var filterNorm = MasterDataStringSearch.NormalizeForContains(input.FilterText);
        input.Sorting = string.IsNullOrWhiteSpace(input.Sorting)
            ? $"{nameof(MediaFile.LastModificationTime)} DESC"
            : input.Sorting;

        var total = await _mediaFileRepository.GetExplorerChildrenCountAsync(parentNorm, filterNorm);
        var items = await _mediaFileRepository.GetExplorerChildrenPagedAsync(
            parentNorm,
            filterNorm,
            input.Sorting,
            input.SkipCount,
            input.MaxResultCount);

        var dtos = ObjectMapper.Map<List<MediaFile>, List<MediaFileDto>>(items);

        await EnrichExplorerReadUrlsAsync(dtos);

        return new PagedResultDto<MediaFileDto>(total, dtos);
    }

    [Authorize(MasterDataServicePermissions.MediaFiles.Create)]
    public virtual async Task<MediaFileDto> CreateFolderAsync(CreateMediaFolderDto input)
    {
        ArgumentNullException.ThrowIfNull(input);
        MediaExplorerPathHelper.ValidateExplorerSegment(input.Name);
        var name = input.Name.Trim();
        var parentNorm = MediaExplorerPathHelper.NormalizeParentFolderPath(input.ParentFolderPath);

        if (await _mediaFileRepository.ExistsExplorerDisplayNameAsync(parentNorm, name))
        {
            throw new UserFriendlyException(L["MediaExplorer:DuplicateName"]);
        }

        var folderLogicalPath = MediaExplorerPathHelper.CombineLogicalChildPath(parentNorm, name);
        Check.Length(folderLogicalPath, nameof(folderLogicalPath), MediaFileConsts.PathMaxLength);

        var folderRecord = await _mediaFileManager.CreateAsync(
            0,
            0,
            0,
            0,
            FileType.Folder,
            FileStatus.Ready,
            fileName: name,
            originalFileName: name,
            extension: null,
            contentType: null,
            storageProvider: "Minio",
            bucket: _configuration["BlobStoring:Minio:BucketName"],
            folder: parentNorm,
            path: folderLogicalPath,
            url: null,
            checksum: null);

        return ObjectMapper.Map<MediaFile, MediaFileDto>(folderRecord);
    }

    [Authorize(MasterDataServicePermissions.MediaFiles.Create)]
    public virtual async Task<MediaFileDto> UploadExplorerFileAsync(
        string? parentFolderPath,
        IRemoteStreamContent content)
    {
        ArgumentNullException.ThrowIfNull(content);
        var opts = _mediaStorageOptions.Value;
        var parentNorm = MediaExplorerPathHelper.NormalizeParentFolderPath(parentFolderPath);

        var originalRaw = content.FileName;
        var originalSafe = Path.GetFileName(originalRaw ?? string.Empty);
        if (string.IsNullOrWhiteSpace(originalSafe))
        {
            throw new UserFriendlyException(L["MediaExplorer:UploadInvalidFile"]);
        }

        MediaExplorerPathHelper.ValidateExplorerSegment(originalSafe);
        if (await _mediaFileRepository.ExistsExplorerDisplayNameAsync(parentNorm, originalSafe.Trim()))
        {
            throw new UserFriendlyException(L["MediaExplorer:DuplicateName"]);
        }

        var extension = Path.GetExtension(originalSafe);
        var blobLeaf = GuidGenerator.Create().ToString("n") + extension;
        var logicalPath = MediaExplorerPathHelper.CombineLogicalChildPath(parentNorm, blobLeaf);
        // IBlobContainer prepends host/ or tenants/{guid}/ — pass logical segments only (e.g. Place/{id}.jpg).
        var minioObjectKey =
            MediaExplorerPathHelper.FormatMinioObjectKey(CurrentTenant.Id, logicalPath);
        Check.Length(minioObjectKey, nameof(minioObjectKey), MediaFileConsts.PathMaxLength);

        var contentTypeHeader = content.ContentType;
        if (content.ContentLength.HasValue && content.ContentLength.Value > opts.MaxUploadBytes)
        {
            throw new UserFriendlyException(L["MediaExplorer:FileTooLarge"]);
        }

        await using var uploadStream = content.GetStream();
        using var buffer = new MemoryStream();
        await CopyUploadWithLimitAsync(uploadStream, buffer, opts.MaxUploadBytes);
        buffer.Position = 0;
        await _mediaFilesBlobContainer.SaveAsync(logicalPath, buffer, overrideExisting: true);

        var publicUrl = MediaExplorerPathHelper.CombinePublicUrl(opts.PublicBaseUrl, minioObjectKey);
        Check.Length(publicUrl ?? "", nameof(publicUrl), MediaFileConsts.UrlMaxLength);

        var fileType = MediaExplorerPathHelper.InferFileTypeFromExtension(extension);
        var contentTypeStored = string.IsNullOrWhiteSpace(contentTypeHeader)
            ? GuessContentTypeForExtension(extension)
            : contentTypeHeader!;

        var entity = await _mediaFileManager.CreateAsync(
            buffer.Length,
            0,
            0,
            0,
            fileType,
            FileStatus.Ready,
            blobLeaf,
            originalSafe.Trim(),
            ExtensionWithoutDot(extension),
            contentTypeStored,
            storageProvider: "Minio",
            bucket: _configuration["BlobStoring:Minio:BucketName"],
            folder: parentNorm,
            path: minioObjectKey,
            url: publicUrl,
            checksum: null);

        return ObjectMapper.Map<MediaFile, MediaFileDto>(entity);
    }

    [Authorize(MasterDataServicePermissions.MediaFiles.Edit)]
    public virtual async Task<MediaFileDto> RenameExplorerItemAsync(Guid id, RenameMediaFileExplorerDto input)
    {
        ArgumentNullException.ThrowIfNull(input);
        MediaExplorerPathHelper.ValidateExplorerSegment(input.DisplayName);

        var name = input.DisplayName.Trim();
        var entity = await _mediaFileRepository.GetAsync(id);

        if (entity.FileType == FileType.Folder)
        {
            throw new UserFriendlyException(L["MediaExplorer:RenameFolderDisabled"]);
        }

        var parentForRename = MediaExplorerPathHelper.NormalizeParentFolderPath(entity.Folder);

        if (await _mediaFileRepository.ExistsExplorerDisplayNameAsync(parentForRename, name, id))
        {
            throw new UserFriendlyException(L["MediaExplorer:DuplicateName"]);
        }

        var updated = await _mediaFileManager.UpdateAsync(
            id,
            entity.Size,
            entity.Width,
            entity.Height,
            entity.Duration,
            entity.FileType,
            entity.Status,
            entity.FileName,
            originalFileName: name,
            entity.Extension,
            entity.ContentType,
            entity.StorageProvider,
            entity.Bucket,
            entity.Folder,
            entity.Path,
            entity.Url,
            entity.Checksum,
            entity.ConcurrencyStamp);

        return ObjectMapper.Map<MediaFile, MediaFileDto>(updated);
    }

    [Authorize(MasterDataServicePermissions.MediaFiles.Delete)]
    public virtual async Task DeleteExplorerEntryAsync(Guid id)
    {
        var entity = await _mediaFileRepository.GetAsync(id);

        if (entity.FileType == FileType.Folder)
        {
            if (string.IsNullOrWhiteSpace(entity.Path))
            {
                throw new UserFriendlyException(L["MediaExplorer:InvalidFolderPath"]);
            }

            var subtree = await _mediaFileRepository.GetSubtreeByPathPrefixDescendingAsync(entity.Path);
            foreach (var row in subtree)
            {
                if (row.FileType != FileType.Folder &&
                    row.Path != null &&
                    !string.IsNullOrWhiteSpace(row.Path))
                {
                    await TryDeleteBlobAsync(row.Path);
                }
            }

            await _mediaFileRepository.DeleteManyAsync(subtree.Select(x => x.Id).ToList());

            return;
        }

        if (entity.Path != null && entity.FileType != FileType.Folder)
        {
            await TryDeleteBlobAsync(entity.Path);
        }

        await _mediaFileRepository.DeleteAsync(entity);
    }

    /// <remarks>
    /// Private MinIO buckets deny anonymous GET; presigned URLs work in browsers for thumbnails and preview.
    /// </remarks>
    private async Task EnrichExplorerReadUrlsAsync(List<MediaFileDto> items)
    {
        if (!_mediaStorageOptions.Value.UsePresignedReadUrls)
        {
            return;
        }

        foreach (var dto in items)
        {
            await EnrichOneExplorerReadUrlAsync(dto);
        }
    }

    private async Task EnrichOneExplorerReadUrlAsync(MediaFileDto dto)
    {
        if (dto.FileType == FileType.Folder)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(dto.Path))
        {
            return;
        }

        if (!(string.Equals(dto.StorageProvider, "Minio", StringComparison.OrdinalIgnoreCase)
              || string.IsNullOrWhiteSpace(dto.StorageProvider)))
        {
            return;
        }

        var defaultBucket = _configuration["BlobStoring:Minio:BucketName"];
        var bucket = (dto.Bucket ?? defaultBucket)?.Trim();

        if (string.IsNullOrWhiteSpace(bucket))
        {
            return;
        }

        var expiry = _mediaStorageOptions.Value.PresignedReadExpirySeconds;
        var signed = await _minioBlobReadUrlSigner.TryGetPresignedReadUrlAsync(bucket!, dto.Path!, expiry)
            .ConfigureAwait(false);

        if (!string.IsNullOrWhiteSpace(signed))
        {
            dto.Url = signed;
        }
    }

    private async Task TryDeleteBlobAsync(string storageKey)
    {
        try
        {
            await _mediaFilesBlobContainer.DeleteAsync(storageKey);
        }
        catch (Exception ex)
        {
            Logger.LogWarning(ex, "Failed to delete blob {StorageKey}", storageKey);
        }
    }

    private async Task CopyUploadWithLimitAsync(Stream source, MemoryStream destination, long maxBytes)
    {
        var buf = new byte[81920];
        long total = 0;
        int read;
        while ((read = await source.ReadAsync(buf.AsMemory(0, buf.Length))) > 0)
        {
            total += read;
            if (total > maxBytes)
            {
                throw new UserFriendlyException(L["MediaExplorer:FileTooLarge"]);
            }

            await destination.WriteAsync(buf.AsMemory(0, read));
        }
    }

    private static string? ExtensionWithoutDot(string extensionWithDotOrEmpty)
    {
        var x = (extensionWithDotOrEmpty ?? string.Empty).Trim();
        if (x.Length <= 1)
        {
            return null;
        }

        var noDot = x.Length > 0 && x[0] == '.' ? x.Substring(1) : x;
        return string.IsNullOrEmpty(noDot) ? null : noDot;
    }

    private static string GuessContentTypeForExtension(string extensionOrName)
    {
        var ext = Path.GetExtension(extensionOrName ?? string.Empty).ToLowerInvariant();
        return ext switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".webp" => "image/webp",
            ".pdf" => "application/pdf",
            ".zip" => "application/zip",
            ".json" => "application/json",
            ".txt" => "text/plain",
            ".csv" => "text/csv",
            ".doc" => "application/msword",
            ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            ".xls" => "application/vnd.ms-excel",
            ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            ".ppt" => "application/vnd.ms-powerpoint",
            ".pptx" => "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            _ => "application/octet-stream"
        };
    }
}
