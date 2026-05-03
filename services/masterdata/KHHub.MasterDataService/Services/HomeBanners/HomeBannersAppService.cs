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
using KHHub.MasterDataService.Services.HomeBanners;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.HomeBanners;
using KHHub.MasterDataService.Services.Dtos.HomeBanners;
using KHHub.MasterDataService.Data.HomeBanners;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.HomeBanners;

[Authorize(MasterDataServicePermissions.HomeBanners.Default)]
public abstract class HomeBannersAppServiceBase : ApplicationService
{
    protected IDistributedCache<HomeBannerDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IHomeBannerRepository _homeBannerRepository;
    protected HomeBannerManager _homeBannerManager;

    public HomeBannersAppServiceBase(IHomeBannerRepository homeBannerRepository, HomeBannerManager homeBannerManager, IDistributedCache<HomeBannerDownloadTokenCacheItem, string> downloadTokenCache)
    {
        _downloadTokenCache = downloadTokenCache;
        _homeBannerRepository = homeBannerRepository;
        _homeBannerManager = homeBannerManager;
    }

    public virtual async Task<PagedResultDto<HomeBannerDto>> GetListAsync(GetHomeBannersInput input)
    {
        var totalCount = await _homeBannerRepository.GetCountAsync(input.FilterText, input.Title, input.Subtitle, input.Description, input.ImageUrl, input.MobileImageUrl, input.ButtonText, input.ButtonUrl, input.TargetType, input.TargetId, input.SortOrderMin, input.SortOrderMax, input.IsActive, input.StartDateMin, input.StartDateMax, input.EndDateMin, input.EndDateMax);
        var items = await _homeBannerRepository.GetListAsync(input.FilterText, input.Title, input.Subtitle, input.Description, input.ImageUrl, input.MobileImageUrl, input.ButtonText, input.ButtonUrl, input.TargetType, input.TargetId, input.SortOrderMin, input.SortOrderMax, input.IsActive, input.StartDateMin, input.StartDateMax, input.EndDateMin, input.EndDateMax, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<HomeBannerDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<HomeBanner>, List<HomeBannerDto>>(items)
        };
    }

    public virtual async Task<HomeBannerDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<HomeBanner, HomeBannerDto>(await _homeBannerRepository.GetAsync(id));
    }

    [Authorize(MasterDataServicePermissions.HomeBanners.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _homeBannerRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.HomeBanners.Create)]
    public virtual async Task<HomeBannerDto> CreateAsync(HomeBannerCreateDto input)
    {
        var homeBanner = await _homeBannerManager.CreateAsync(input.Title, input.ImageUrl, input.SortOrder, input.IsActive, input.Subtitle, input.Description, input.MobileImageUrl, input.ButtonText, input.ButtonUrl, input.TargetType, input.TargetId, input.StartDate, input.EndDate);
        return ObjectMapper.Map<HomeBanner, HomeBannerDto>(homeBanner);
    }

    [Authorize(MasterDataServicePermissions.HomeBanners.Edit)]
    public virtual async Task<HomeBannerDto> UpdateAsync(Guid id, HomeBannerUpdateDto input)
    {
        var homeBanner = await _homeBannerManager.UpdateAsync(id, input.Title, input.ImageUrl, input.SortOrder, input.IsActive, input.Subtitle, input.Description, input.MobileImageUrl, input.ButtonText, input.ButtonUrl, input.TargetType, input.TargetId, input.StartDate, input.EndDate, input.ConcurrencyStamp);
        return ObjectMapper.Map<HomeBanner, HomeBannerDto>(homeBanner);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(HomeBannerExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var items = await _homeBannerRepository.GetListAsync(input.FilterText, input.Title, input.Subtitle, input.Description, input.ImageUrl, input.MobileImageUrl, input.ButtonText, input.ButtonUrl, input.TargetType, input.TargetId, input.SortOrderMin, input.SortOrderMax, input.IsActive, input.StartDateMin, input.StartDateMax, input.EndDateMin, input.EndDateMax);
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<HomeBanner>, List<HomeBannerExcelDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "HomeBanners.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.HomeBanners.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> homebannerIds)
    {
        await _homeBannerRepository.DeleteManyAsync(homebannerIds);
    }

    [Authorize(MasterDataServicePermissions.HomeBanners.Delete)]
    public virtual async Task DeleteAllAsync(GetHomeBannersInput input)
    {
        await _homeBannerRepository.DeleteAllAsync(input.FilterText, input.Title, input.Subtitle, input.Description, input.ImageUrl, input.MobileImageUrl, input.ButtonText, input.ButtonUrl, input.TargetType, input.TargetId, input.SortOrderMin, input.SortOrderMax, input.IsActive, input.StartDateMin, input.StartDateMax, input.EndDateMin, input.EndDateMax);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new HomeBannerDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}