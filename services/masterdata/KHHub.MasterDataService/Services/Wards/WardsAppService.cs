using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Provinces;
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
using KHHub.MasterDataService.Services.Wards;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.Wards;
using KHHub.MasterDataService.Services.Dtos.Wards;
using KHHub.MasterDataService.Data.Wards;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.Wards;

[Authorize(MasterDataServicePermissions.Wards.Default)]
public abstract class WardsAppServiceBase : ApplicationService
{
    protected IDistributedCache<WardDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IWardRepository _wardRepository;
    protected WardManager _wardManager;
    protected IRepository<KHHub.MasterDataService.Entities.Provinces.Province, Guid> _provinceRepository;

    public WardsAppServiceBase(IWardRepository wardRepository, WardManager wardManager, IDistributedCache<WardDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.Provinces.Province, Guid> provinceRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _wardRepository = wardRepository;
        _wardManager = wardManager;
        _provinceRepository = provinceRepository;
    }

    public virtual async Task<PagedResultDto<WardWithNavigationPropertiesDto>> GetListAsync(GetWardsInput input)
    {
        var totalCount = await _wardRepository.GetCountAsync(input.FilterText, input.Code, input.Name, input.ProvinceId);
        var items = await _wardRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.ProvinceId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<WardWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<WardWithNavigationProperties>, List<WardWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<WardWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<WardWithNavigationProperties, WardWithNavigationPropertiesDto>(await _wardRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<WardDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<Ward, WardDto>(await _wardRepository.GetAsync(id));
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetProvinceLookupAsync(LookupRequestDto input)
    {
        var query = (await _provinceRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name != null && x.Name.Contains(input.Filter));
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.Provinces.Province>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.Name }).ToList()
        };
    }

    [Authorize(MasterDataServicePermissions.Wards.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _wardRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.Wards.Create)]
    public virtual async Task<WardDto> CreateAsync(WardCreateDto input)
    {
        if (input.ProvinceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Province"]]);
        }

        var ward = await _wardManager.CreateAsync(input.ProvinceId, input.Code, input.Name);
        return ObjectMapper.Map<Ward, WardDto>(ward);
    }

    [Authorize(MasterDataServicePermissions.Wards.Edit)]
    public virtual async Task<WardDto> UpdateAsync(Guid id, WardUpdateDto input)
    {
        if (input.ProvinceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Province"]]);
        }

        var ward = await _wardManager.UpdateAsync(id, input.ProvinceId, input.Code, input.Name, input.ConcurrencyStamp);
        return ObjectMapper.Map<Ward, WardDto>(ward);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(WardExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var wards = await _wardRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Code, input.Name, input.ProvinceId);
        var items = wards.Select(item => new { Code = item.Ward.Code, Name = item.Ward.Name, Province = item.Province?.Name, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "Wards.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.Wards.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> wardIds)
    {
        await _wardRepository.DeleteManyAsync(wardIds);
    }

    [Authorize(MasterDataServicePermissions.Wards.Delete)]
    public virtual async Task DeleteAllAsync(GetWardsInput input)
    {
        await _wardRepository.DeleteAllAsync(input.FilterText, input.Code, input.Name, input.ProvinceId);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new WardDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}