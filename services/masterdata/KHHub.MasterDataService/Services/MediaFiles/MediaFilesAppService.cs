using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using KHHub.MasterDataService.Permissions;
using KHHub.MasterDataService.Services.MediaFiles;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.MediaFiles;
using KHHub.MasterDataService.Services.Dtos.MediaFiles;
using KHHub.MasterDataService.Data.MediaFiles;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.MediaFiles;

[Authorize(MasterDataServicePermissions.MediaFiles.Default)]
public abstract class MediaFilesAppServiceBase : ApplicationService
{
    protected IDistributedCache<MediaFileDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IMediaFileRepository _mediaFileRepository;
    protected MediaFileManager _mediaFileManager;

    public MediaFilesAppServiceBase(IMediaFileRepository mediaFileRepository, MediaFileManager mediaFileManager, IDistributedCache<MediaFileDownloadTokenCacheItem, string> downloadTokenCache)
    {
        _downloadTokenCache = downloadTokenCache;
        _mediaFileRepository = mediaFileRepository;
        _mediaFileManager = mediaFileManager;
    }

    public virtual async Task<PagedResultDto<MediaFileDto>> GetListAsync(GetMediaFilesInput input)
    {
        var totalCount = await _mediaFileRepository.GetCountAsync(input.FilterText, input.FileName, input.OriginalFileName, input.Extension, input.ContentType, input.StorageProvider, input.Bucket, input.Folder, input.Path, input.Url, input.Checksum, input.WidthMin, input.WidthMax, input.HeightMin, input.HeightMax, input.FileType, input.Status);
        var items = await _mediaFileRepository.GetListAsync(input.FilterText, input.FileName, input.OriginalFileName, input.Extension, input.ContentType, input.StorageProvider, input.Bucket, input.Folder, input.Path, input.Url, input.Checksum, input.WidthMin, input.WidthMax, input.HeightMin, input.HeightMax, input.FileType, input.Status, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<MediaFileDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<MediaFile>, List<MediaFileDto>>(items)
        };
    }

    public virtual async Task<MediaFileDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<MediaFile, MediaFileDto>(await _mediaFileRepository.GetAsync(id));
    }

    [Authorize(MasterDataServicePermissions.MediaFiles.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _mediaFileRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.MediaFiles.Create)]
    public virtual async Task<MediaFileDto> CreateAsync(MediaFileCreateDto input)
    {
        var mediaFile = await _mediaFileManager.CreateAsync(input.Size, input.Width, input.Height, input.Duration, input.FileType, input.Status, input.FileName, input.OriginalFileName, input.Extension, input.ContentType, input.StorageProvider, input.Bucket, input.Folder, input.Path, input.Url, input.Checksum);
        return ObjectMapper.Map<MediaFile, MediaFileDto>(mediaFile);
    }

    [Authorize(MasterDataServicePermissions.MediaFiles.Edit)]
    public virtual async Task<MediaFileDto> UpdateAsync(Guid id, MediaFileUpdateDto input)
    {
        var mediaFile = await _mediaFileManager.UpdateAsync(id, input.Size, input.Width, input.Height, input.Duration, input.FileType, input.Status, input.FileName, input.OriginalFileName, input.Extension, input.ContentType, input.StorageProvider, input.Bucket, input.Folder, input.Path, input.Url, input.Checksum, input.ConcurrencyStamp);
        return ObjectMapper.Map<MediaFile, MediaFileDto>(mediaFile);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(MediaFileExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var items = await _mediaFileRepository.GetListAsync(input.FilterText, input.FileName, input.OriginalFileName, input.Extension, input.ContentType, input.StorageProvider, input.Bucket, input.Folder, input.Path, input.Url, input.Checksum, input.WidthMin, input.WidthMax, input.HeightMin, input.HeightMax, input.FileType, input.Status);
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<MediaFile>, List<MediaFileExcelDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "MediaFiles.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.MediaFiles.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> mediafileIds)
    {
        await _mediaFileRepository.DeleteManyAsync(mediafileIds);
    }

    [Authorize(MasterDataServicePermissions.MediaFiles.Delete)]
    public virtual async Task DeleteAllAsync(GetMediaFilesInput input)
    {
        await _mediaFileRepository.DeleteAllAsync(input.FilterText, input.FileName, input.OriginalFileName, input.Extension, input.ContentType, input.StorageProvider, input.Bucket, input.Folder, input.Path, input.Url, input.Checksum, input.WidthMin, input.WidthMax, input.HeightMin, input.HeightMax, input.FileType, input.Status);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new MediaFileDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}