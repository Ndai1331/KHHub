using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.MediaFiles;
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
using KHHub.MasterDataService.Services.EntityFiles;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.EntityFiles;
using KHHub.MasterDataService.Services.Dtos.EntityFiles;
using KHHub.MasterDataService.Data.EntityFiles;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.EntityFiles;

[Authorize(MasterDataServicePermissions.EntityFiles.Default)]
public abstract class EntityFilesAppServiceBase : ApplicationService
{
    protected IDistributedCache<EntityFileDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IEntityFileRepository _entityFileRepository;
    protected EntityFileManager _entityFileManager;
    protected IRepository<KHHub.MasterDataService.Entities.MediaFiles.MediaFile, Guid> _mediaFileRepository;

    public EntityFilesAppServiceBase(IEntityFileRepository entityFileRepository, EntityFileManager entityFileManager, IDistributedCache<EntityFileDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.MediaFiles.MediaFile, Guid> mediaFileRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _entityFileRepository = entityFileRepository;
        _entityFileManager = entityFileManager;
        _mediaFileRepository = mediaFileRepository;
    }

    public virtual async Task<PagedResultDto<EntityFileWithNavigationPropertiesDto>> GetListAsync(GetEntityFilesInput input)
    {
        var totalCount = await _entityFileRepository.GetCountAsync(input.FilterText, input.EntityType, input.EntityId, input.Collection, input.SortOrderMin, input.SortOrderMax, input.IsPrimary, input.MediaFileId);
        var items = await _entityFileRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.EntityType, input.EntityId, input.Collection, input.SortOrderMin, input.SortOrderMax, input.IsPrimary, input.MediaFileId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<EntityFileWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<EntityFileWithNavigationProperties>, List<EntityFileWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<EntityFileWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<EntityFileWithNavigationProperties, EntityFileWithNavigationPropertiesDto>(await _entityFileRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<EntityFileDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<EntityFile, EntityFileDto>(await _entityFileRepository.GetAsync(id));
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetMediaFileLookupAsync(LookupRequestDto input)
    {
        var query = (await _mediaFileRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.FileName != null && x.FileName.Contains(input.Filter));
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.MediaFiles.MediaFile>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.FileName }).ToList()
        };
    }

    [Authorize(MasterDataServicePermissions.EntityFiles.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _entityFileRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.EntityFiles.Create)]
    public virtual async Task<EntityFileDto> CreateAsync(EntityFileCreateDto input)
    {
        if (input.MediaFileId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["MediaFile"]]);
        }

        var entityFile = await _entityFileManager.CreateAsync(input.MediaFileId, input.EntityType, input.EntityId, input.SortOrder, input.IsPrimary, input.Collection);
        return ObjectMapper.Map<EntityFile, EntityFileDto>(entityFile);
    }

    [Authorize(MasterDataServicePermissions.EntityFiles.Edit)]
    public virtual async Task<EntityFileDto> UpdateAsync(Guid id, EntityFileUpdateDto input)
    {
        if (input.MediaFileId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["MediaFile"]]);
        }

        var entityFile = await _entityFileManager.UpdateAsync(id, input.MediaFileId, input.EntityType, input.EntityId, input.SortOrder, input.IsPrimary, input.Collection, input.ConcurrencyStamp);
        return ObjectMapper.Map<EntityFile, EntityFileDto>(entityFile);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(EntityFileExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var entityFiles = await _entityFileRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.EntityType, input.EntityId, input.Collection, input.SortOrderMin, input.SortOrderMax, input.IsPrimary, input.MediaFileId);
        var items = entityFiles.Select(item => new { EntityType = item.EntityFile.EntityType, EntityId = item.EntityFile.EntityId, Collection = item.EntityFile.Collection, SortOrder = item.EntityFile.SortOrder, IsPrimary = item.EntityFile.IsPrimary, MediaFile = item.MediaFile?.FileName, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "EntityFiles.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.EntityFiles.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> entityfileIds)
    {
        await _entityFileRepository.DeleteManyAsync(entityfileIds);
    }

    [Authorize(MasterDataServicePermissions.EntityFiles.Delete)]
    public virtual async Task DeleteAllAsync(GetEntityFilesInput input)
    {
        await _entityFileRepository.DeleteAllAsync(input.FilterText, input.EntityType, input.EntityId, input.Collection, input.SortOrderMin, input.SortOrderMax, input.IsPrimary, input.MediaFileId);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new EntityFileDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}