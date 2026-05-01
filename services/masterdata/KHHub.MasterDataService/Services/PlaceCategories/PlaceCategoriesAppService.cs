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
using KHHub.MasterDataService.Services.PlaceCategories;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.PlaceCategories;
using KHHub.MasterDataService.Services.Dtos.PlaceCategories;
using KHHub.MasterDataService.Data.PlaceCategories;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.PlaceCategories;

[Authorize(MasterDataServicePermissions.PlaceCategories.Default)]
public abstract class PlaceCategoriesAppServiceBase : ApplicationService
{
    protected IDistributedCache<PlaceCategoryDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IPlaceCategoryRepository _placeCategoryRepository;
    protected PlaceCategoryManager _placeCategoryManager;

    public PlaceCategoriesAppServiceBase(IPlaceCategoryRepository placeCategoryRepository, PlaceCategoryManager placeCategoryManager, IDistributedCache<PlaceCategoryDownloadTokenCacheItem, string> downloadTokenCache)
    {
        _downloadTokenCache = downloadTokenCache;
        _placeCategoryRepository = placeCategoryRepository;
        _placeCategoryManager = placeCategoryManager;
    }

    public virtual async Task<PagedResultDto<PlaceCategoryDto>> GetListAsync(GetPlaceCategoriesInput input)
    {
        var totalCount = await _placeCategoryRepository.GetCountAsync(input.FilterText, input.Name, input.Slug, input.Description, input.Icon, input.Color, input.ParentId, input.DisplayOrderMin, input.DisplayOrderMax, input.IsActive);
        var items = await _placeCategoryRepository.GetListAsync(input.FilterText, input.Name, input.Slug, input.Description, input.Icon, input.Color, input.ParentId, input.DisplayOrderMin, input.DisplayOrderMax, input.IsActive, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<PlaceCategoryDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<PlaceCategory>, List<PlaceCategoryDto>>(items)
        };
    }

    public virtual async Task<PlaceCategoryDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<PlaceCategory, PlaceCategoryDto>(await _placeCategoryRepository.GetAsync(id));
    }

    [Authorize(MasterDataServicePermissions.PlaceCategories.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _placeCategoryRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.PlaceCategories.Create)]
    public virtual async Task<PlaceCategoryDto> CreateAsync(PlaceCategoryCreateDto input)
    {
        var placeCategory = await _placeCategoryManager.CreateAsync(input.Name, input.Slug, input.ParentId, input.DisplayOrder, input.IsActive, input.Description, input.Icon, input.Color);
        return ObjectMapper.Map<PlaceCategory, PlaceCategoryDto>(placeCategory);
    }

    [Authorize(MasterDataServicePermissions.PlaceCategories.Edit)]
    public virtual async Task<PlaceCategoryDto> UpdateAsync(Guid id, PlaceCategoryUpdateDto input)
    {
        var placeCategory = await _placeCategoryManager.UpdateAsync(id, input.Name, input.Slug, input.ParentId, input.DisplayOrder, input.IsActive, input.Description, input.Icon, input.Color, input.ConcurrencyStamp);
        return ObjectMapper.Map<PlaceCategory, PlaceCategoryDto>(placeCategory);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PlaceCategoryExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var items = await _placeCategoryRepository.GetListAsync(input.FilterText, input.Name, input.Slug, input.Description, input.Icon, input.Color, input.ParentId, input.DisplayOrderMin, input.DisplayOrderMax, input.IsActive);
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(ObjectMapper.Map<List<PlaceCategory>, List<PlaceCategoryExcelDto>>(items));
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "PlaceCategories.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.PlaceCategories.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> placecategoryIds)
    {
        await _placeCategoryRepository.DeleteManyAsync(placecategoryIds);
    }

    [Authorize(MasterDataServicePermissions.PlaceCategories.Delete)]
    public virtual async Task DeleteAllAsync(GetPlaceCategoriesInput input)
    {
        await _placeCategoryRepository.DeleteAllAsync(input.FilterText, input.Name, input.Slug, input.Description, input.Icon, input.Color, input.ParentId, input.DisplayOrderMin, input.DisplayOrderMax, input.IsActive);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new PlaceCategoryDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}