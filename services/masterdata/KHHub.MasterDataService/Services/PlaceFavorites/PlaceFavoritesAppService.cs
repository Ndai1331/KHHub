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
using KHHub.MasterDataService.Services.PlaceFavorites;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.PlaceFavorites;
using KHHub.MasterDataService.Services.Dtos.PlaceFavorites;
using KHHub.MasterDataService.Data.PlaceFavorites;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.PlaceFavorites;

[Authorize(MasterDataServicePermissions.PlaceFavorites.Default)]
public abstract class PlaceFavoritesAppServiceBase : ApplicationService
{
    protected IDistributedCache<PlaceFavoriteDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IPlaceFavoriteRepository _placeFavoriteRepository;
    protected PlaceFavoriteManager _placeFavoriteManager;
    protected IRepository<KHHub.MasterDataService.Entities.Places.Place, Guid> _placeRepository;

    public PlaceFavoritesAppServiceBase(IPlaceFavoriteRepository placeFavoriteRepository, PlaceFavoriteManager placeFavoriteManager, IDistributedCache<PlaceFavoriteDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.Places.Place, Guid> placeRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _placeFavoriteRepository = placeFavoriteRepository;
        _placeFavoriteManager = placeFavoriteManager;
        _placeRepository = placeRepository;
    }

    public virtual async Task<PagedResultDto<PlaceFavoriteWithNavigationPropertiesDto>> GetListAsync(GetPlaceFavoritesInput input)
    {
        var totalCount = await _placeFavoriteRepository.GetCountAsync(input.FilterText, input.UserId, input.PlaceId);
        var items = await _placeFavoriteRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UserId, input.PlaceId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<PlaceFavoriteWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<PlaceFavoriteWithNavigationProperties>, List<PlaceFavoriteWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<PlaceFavoriteWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<PlaceFavoriteWithNavigationProperties, PlaceFavoriteWithNavigationPropertiesDto>(await _placeFavoriteRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<PlaceFavoriteDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<PlaceFavorite, PlaceFavoriteDto>(await _placeFavoriteRepository.GetAsync(id));
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

    [Authorize(MasterDataServicePermissions.PlaceFavorites.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _placeFavoriteRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.PlaceFavorites.Create)]
    public virtual async Task<PlaceFavoriteDto> CreateAsync(PlaceFavoriteCreateDto input)
    {
        if (input.PlaceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Place"]]);
        }

        var placeFavorite = await _placeFavoriteManager.CreateAsync(input.PlaceId, input.UserId);
        return ObjectMapper.Map<PlaceFavorite, PlaceFavoriteDto>(placeFavorite);
    }

    [Authorize(MasterDataServicePermissions.PlaceFavorites.Edit)]
    public virtual async Task<PlaceFavoriteDto> UpdateAsync(Guid id, PlaceFavoriteUpdateDto input)
    {
        if (input.PlaceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Place"]]);
        }

        var placeFavorite = await _placeFavoriteManager.UpdateAsync(id, input.PlaceId, input.UserId, input.ConcurrencyStamp);
        return ObjectMapper.Map<PlaceFavorite, PlaceFavoriteDto>(placeFavorite);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PlaceFavoriteExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var placeFavorites = await _placeFavoriteRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.UserId, input.PlaceId);
        var items = placeFavorites.Select(item => new { UserId = item.PlaceFavorite.UserId, Place = item.Place?.Name, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "PlaceFavorites.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.PlaceFavorites.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> placefavoriteIds)
    {
        await _placeFavoriteRepository.DeleteManyAsync(placefavoriteIds);
    }

    [Authorize(MasterDataServicePermissions.PlaceFavorites.Delete)]
    public virtual async Task DeleteAllAsync(GetPlaceFavoritesInput input)
    {
        await _placeFavoriteRepository.DeleteAllAsync(input.FilterText, input.UserId, input.PlaceId);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new PlaceFavoriteDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}