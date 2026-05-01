using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Places;
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
using KHHub.MasterDataService.Services.PlaceViews;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.PlaceViews;
using KHHub.MasterDataService.Services.Dtos.PlaceViews;
using KHHub.MasterDataService.Data.PlaceViews;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.PlaceViews;

[Authorize(MasterDataServicePermissions.PlaceViews.Default)]
public abstract class PlaceViewsAppServiceBase : ApplicationService
{
    protected IDistributedCache<PlaceViewDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IPlaceViewRepository _placeViewRepository;
    protected PlaceViewManager _placeViewManager;
    protected IRepository<KHHub.MasterDataService.Entities.Places.Place, Guid> _placeRepository;

    public PlaceViewsAppServiceBase(IPlaceViewRepository placeViewRepository, PlaceViewManager placeViewManager, IDistributedCache<PlaceViewDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.Places.Place, Guid> placeRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _placeViewRepository = placeViewRepository;
        _placeViewManager = placeViewManager;
        _placeRepository = placeRepository;
    }

    public virtual async Task<PagedResultDto<PlaceViewWithNavigationPropertiesDto>> GetListAsync(GetPlaceViewsInput input)
    {
        var totalCount = await _placeViewRepository.GetCountAsync(input.FilterText, input.UserId, input.IpAddress, input.Device, input.ViewedAtMin, input.ViewedAtMax, input.DurationMin, input.DurationMax, input.Source, input.PlaceId);
        var items = await _placeViewRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UserId, input.IpAddress, input.Device, input.ViewedAtMin, input.ViewedAtMax, input.DurationMin, input.DurationMax, input.Source, input.PlaceId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<PlaceViewWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<PlaceViewWithNavigationProperties>, List<PlaceViewWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<PlaceViewWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<PlaceViewWithNavigationProperties, PlaceViewWithNavigationPropertiesDto>(await _placeViewRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<PlaceViewDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<PlaceView, PlaceViewDto>(await _placeViewRepository.GetAsync(id));
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPlaceLookupAsync(LookupRequestDto input)
    {
        var query = (await _placeRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name != null && x.Name.Contains(input.Filter));
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.Places.Place>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.Name }).ToList()
        };
    }

    [Authorize(MasterDataServicePermissions.PlaceViews.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _placeViewRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.PlaceViews.Create)]
    public virtual async Task<PlaceViewDto> CreateAsync(PlaceViewCreateDto input)
    {
        if (input.PlaceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Place"]]);
        }

        var placeView = await _placeViewManager.CreateAsync(input.PlaceId, input.UserId, input.ViewedAt, input.Duration, input.IpAddress, input.Device, input.Source);
        return ObjectMapper.Map<PlaceView, PlaceViewDto>(placeView);
    }

    [Authorize(MasterDataServicePermissions.PlaceViews.Edit)]
    public virtual async Task<PlaceViewDto> UpdateAsync(Guid id, PlaceViewUpdateDto input)
    {
        if (input.PlaceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Place"]]);
        }

        var placeView = await _placeViewManager.UpdateAsync(id, input.PlaceId, input.UserId, input.ViewedAt, input.Duration, input.IpAddress, input.Device, input.Source, input.ConcurrencyStamp);
        return ObjectMapper.Map<PlaceView, PlaceViewDto>(placeView);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PlaceViewExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var placeViews = await _placeViewRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UserId, input.IpAddress, input.Device, input.ViewedAtMin, input.ViewedAtMax, input.DurationMin, input.DurationMax, input.Source, input.PlaceId);
        var items = placeViews.Select(item => new { UserId = item.PlaceView.UserId, IpAddress = item.PlaceView.IpAddress, Device = item.PlaceView.Device, ViewedAt = item.PlaceView.ViewedAt, Duration = item.PlaceView.Duration, Source = item.PlaceView.Source, Place = item.Place?.Name, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "PlaceViews.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.PlaceViews.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> placeviewIds)
    {
        await _placeViewRepository.DeleteManyAsync(placeviewIds);
    }

    [Authorize(MasterDataServicePermissions.PlaceViews.Delete)]
    public virtual async Task DeleteAllAsync(GetPlaceViewsInput input)
    {
        await _placeViewRepository.DeleteAllAsync(input.FilterText, input.UserId, input.IpAddress, input.Device, input.ViewedAtMin, input.ViewedAtMax, input.DurationMin, input.DurationMax, input.Source, input.PlaceId);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new PlaceViewDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}