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
using KHHub.MasterDataService.Services.PlaceTags;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.PlaceTags;
using KHHub.MasterDataService.Services.Dtos.PlaceTags;
using KHHub.MasterDataService.Data.PlaceTags;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.PlaceTags;

[Authorize(MasterDataServicePermissions.PlaceTags.Default)]
public abstract class PlaceTagsAppServiceBase : ApplicationService
{
    protected IDistributedCache<PlaceTagDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IPlaceTagRepository _placeTagRepository;
    protected PlaceTagManager _placeTagManager;

    public PlaceTagsAppServiceBase(IPlaceTagRepository placeTagRepository, PlaceTagManager placeTagManager, IDistributedCache<PlaceTagDownloadTokenCacheItem, string> downloadTokenCache)
    {
        _downloadTokenCache = downloadTokenCache;
        _placeTagRepository = placeTagRepository;
        _placeTagManager = placeTagManager;
    }

    public virtual async Task<PagedResultDto<PlaceTagDto>> GetListAsync(GetPlaceTagsInput input)
    {
        var totalCount = await _placeTagRepository.GetCountAsync(input.FilterText, input.Name, input.Slug, input.Description, input.UsageCountMin, input.UsageCountMax);
        var items = await _placeTagRepository.GetListAsync(input.FilterText, input.Name, input.Slug, input.Description, input.UsageCountMin, input.UsageCountMax, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<PlaceTagDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<PlaceTag>, List<PlaceTagDto>>(items)
        };
    }

    public virtual async Task<PlaceTagDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<PlaceTag, PlaceTagDto>(await _placeTagRepository.GetAsync(id));
    }

    [Authorize(MasterDataServicePermissions.PlaceTags.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _placeTagRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.PlaceTags.Create)]
    public virtual async Task<PlaceTagDto> CreateAsync(PlaceTagCreateDto input)
    {
        var placeTag = await _placeTagManager.CreateAsync(input.Name, input.Slug, input.UsageCount, input.Description);
        return ObjectMapper.Map<PlaceTag, PlaceTagDto>(placeTag);
    }

    [Authorize(MasterDataServicePermissions.PlaceTags.Edit)]
    public virtual async Task<PlaceTagDto> UpdateAsync(Guid id, PlaceTagUpdateDto input)
    {
        var placeTag = await _placeTagManager.UpdateAsync(id, input.Name, input.Slug, input.UsageCount, input.Description, input.ConcurrencyStamp);
        return ObjectMapper.Map<PlaceTag, PlaceTagDto>(placeTag);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PlaceTagExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var items = await _placeTagRepository.GetListAsync(input.FilterText, input.Name, input.Slug, input.Description, input.UsageCountMin, input.UsageCountMax);
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<PlaceTag>, List<PlaceTagExcelDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "PlaceTags.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.PlaceTags.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> placetagIds)
    {
        await _placeTagRepository.DeleteManyAsync(placetagIds);
    }

    [Authorize(MasterDataServicePermissions.PlaceTags.Delete)]
    public virtual async Task DeleteAllAsync(GetPlaceTagsInput input)
    {
        await _placeTagRepository.DeleteAllAsync(input.FilterText, input.Name, input.Slug, input.Description, input.UsageCountMin, input.UsageCountMax);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new PlaceTagDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}