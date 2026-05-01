using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Entities.PlaceTags;
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
using KHHub.MasterDataService.Services.PlaceTagMappings;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.PlaceTagMappings;
using KHHub.MasterDataService.Services.Dtos.PlaceTagMappings;
using KHHub.MasterDataService.Data.PlaceTagMappings;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.PlaceTagMappings;

[Authorize(MasterDataServicePermissions.PlaceTagMappings.Default)]
public abstract class PlaceTagMappingsAppServiceBase : ApplicationService
{
    protected IDistributedCache<PlaceTagMappingDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IPlaceTagMappingRepository _placeTagMappingRepository;
    protected PlaceTagMappingManager _placeTagMappingManager;
    protected IRepository<KHHub.MasterDataService.Entities.PlaceTags.PlaceTag, Guid> _placeTagRepository;
    protected IRepository<KHHub.MasterDataService.Entities.Places.Place, Guid> _placeRepository;

    public PlaceTagMappingsAppServiceBase(IPlaceTagMappingRepository placeTagMappingRepository, PlaceTagMappingManager placeTagMappingManager, IDistributedCache<PlaceTagMappingDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.PlaceTags.PlaceTag, Guid> placeTagRepository, IRepository<KHHub.MasterDataService.Entities.Places.Place, Guid> placeRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _placeTagMappingRepository = placeTagMappingRepository;
        _placeTagMappingManager = placeTagMappingManager;
        _placeTagRepository = placeTagRepository;
        _placeRepository = placeRepository;
    }

    public virtual async Task<PagedResultDto<PlaceTagMappingWithNavigationPropertiesDto>> GetListAsync(GetPlaceTagMappingsInput input)
    {
        var totalCount = await _placeTagMappingRepository.GetCountAsync(input.FilterText, input.IsPrimary, input.SortOrderMin, input.SortOrderMax, input.PlaceTagId, input.PlaceId);
        var items = await _placeTagMappingRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.IsPrimary, input.SortOrderMin, input.SortOrderMax, input.PlaceTagId, input.PlaceId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<PlaceTagMappingWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<PlaceTagMappingWithNavigationProperties>, List<PlaceTagMappingWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<PlaceTagMappingWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<PlaceTagMappingWithNavigationProperties, PlaceTagMappingWithNavigationPropertiesDto>(await _placeTagMappingRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<PlaceTagMappingDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<PlaceTagMapping, PlaceTagMappingDto>(await _placeTagMappingRepository.GetAsync(id));
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPlaceTagLookupAsync(LookupRequestDto input)
    {
        var query = (await _placeTagRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name != null && x.Name.Contains(input.Filter));
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.PlaceTags.PlaceTag>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.Name }).ToList()
        };
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

    [Authorize(MasterDataServicePermissions.PlaceTagMappings.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _placeTagMappingRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.PlaceTagMappings.Create)]
    public virtual async Task<PlaceTagMappingDto> CreateAsync(PlaceTagMappingCreateDto input)
    {
        if (input.PlaceTagId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["PlaceTag"]]);
        }

        if (input.PlaceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Place"]]);
        }

        var placeTagMapping = await _placeTagMappingManager.CreateAsync(input.PlaceTagId, input.PlaceId, input.IsPrimary, input.SortOrder);
        return ObjectMapper.Map<PlaceTagMapping, PlaceTagMappingDto>(placeTagMapping);
    }

    [Authorize(MasterDataServicePermissions.PlaceTagMappings.Edit)]
    public virtual async Task<PlaceTagMappingDto> UpdateAsync(Guid id, PlaceTagMappingUpdateDto input)
    {
        if (input.PlaceTagId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["PlaceTag"]]);
        }

        if (input.PlaceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Place"]]);
        }

        var placeTagMapping = await _placeTagMappingManager.UpdateAsync(id, input.PlaceTagId, input.PlaceId, input.IsPrimary, input.SortOrder, input.ConcurrencyStamp);
        return ObjectMapper.Map<PlaceTagMapping, PlaceTagMappingDto>(placeTagMapping);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PlaceTagMappingExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var placeTagMappings = await _placeTagMappingRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.IsPrimary, input.SortOrderMin, input.SortOrderMax, input.PlaceTagId, input.PlaceId);
        var items = placeTagMappings.Select(item => new { IsPrimary = item.PlaceTagMapping.IsPrimary, SortOrder = item.PlaceTagMapping.SortOrder, PlaceTag = item.PlaceTag?.Name, Place = item.Place?.Name, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "PlaceTagMappings.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.PlaceTagMappings.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> placetagmappingIds)
    {
        await _placeTagMappingRepository.DeleteManyAsync(placetagmappingIds);
    }

    [Authorize(MasterDataServicePermissions.PlaceTagMappings.Delete)]
    public virtual async Task DeleteAllAsync(GetPlaceTagMappingsInput input)
    {
        await _placeTagMappingRepository.DeleteAllAsync(input.FilterText, input.IsPrimary, input.SortOrderMin, input.SortOrderMax, input.PlaceTagId, input.PlaceId);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new PlaceTagMappingDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}