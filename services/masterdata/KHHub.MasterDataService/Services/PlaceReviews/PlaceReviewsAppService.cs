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
using KHHub.MasterDataService.Services.PlaceReviews;
using MiniExcelLibs;
using Volo.Abp.Content;
using Volo.Abp.Authorization;
using Volo.Abp.Caching;
using Microsoft.Extensions.Caching.Distributed;
using KHHub.MasterDataService.Entities.PlaceReviews;
using KHHub.MasterDataService.Services.Dtos.PlaceReviews;
using KHHub.MasterDataService.Data.PlaceReviews;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.PlaceReviews;

[Authorize(MasterDataServicePermissions.PlaceReviews.Default)]
public abstract class PlaceReviewsAppServiceBase : ApplicationService
{
    protected IDistributedCache<PlaceReviewDownloadTokenCacheItem, string> _downloadTokenCache;
    protected IPlaceReviewRepository _placeReviewRepository;
    protected PlaceReviewManager _placeReviewManager;
    protected IRepository<KHHub.MasterDataService.Entities.Places.Place, Guid> _placeRepository;

    public PlaceReviewsAppServiceBase(IPlaceReviewRepository placeReviewRepository, PlaceReviewManager placeReviewManager, IDistributedCache<PlaceReviewDownloadTokenCacheItem, string> downloadTokenCache, IRepository<KHHub.MasterDataService.Entities.Places.Place, Guid> placeRepository)
    {
        _downloadTokenCache = downloadTokenCache;
        _placeReviewRepository = placeReviewRepository;
        _placeReviewManager = placeReviewManager;
        _placeRepository = placeRepository;
    }

    public virtual async Task<PagedResultDto<PlaceReviewWithNavigationPropertiesDto>> GetListAsync(GetPlaceReviewsInput input)
    {
        var totalCount = await _placeReviewRepository.GetCountAsync(input.FilterText, input.RatingMin, input.RatingMax, input.Title, input.Comment, input.LikeCountMin, input.LikeCountMax, input.Status, input.UserId, input.PlaceId);
        var items = await _placeReviewRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.RatingMin, input.RatingMax, input.Title, input.Comment, input.LikeCountMin, input.LikeCountMax, input.Status, input.UserId, input.PlaceId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<PlaceReviewWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<PlaceReviewWithNavigationProperties>, List<PlaceReviewWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<PlaceReviewWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<PlaceReviewWithNavigationProperties, PlaceReviewWithNavigationPropertiesDto>(await _placeReviewRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<PlaceReviewDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<PlaceReview, PlaceReviewDto>(await _placeReviewRepository.GetAsync(id));
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

    [Authorize(MasterDataServicePermissions.PlaceReviews.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _placeReviewRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.PlaceReviews.Create)]
    public virtual async Task<PlaceReviewDto> CreateAsync(PlaceReviewCreateDto input)
    {
        if (input.PlaceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Place"]]);
        }

        var placeReview = await _placeReviewManager.CreateAsync(input.PlaceId, input.Rating, input.Title, input.LikeCount, input.Status, input.Comment, input.UserId);
        return ObjectMapper.Map<PlaceReview, PlaceReviewDto>(placeReview);
    }

    [Authorize(MasterDataServicePermissions.PlaceReviews.Edit)]
    public virtual async Task<PlaceReviewDto> UpdateAsync(Guid id, PlaceReviewUpdateDto input)
    {
        if (input.PlaceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Place"]]);
        }

        var placeReview = await _placeReviewManager.UpdateAsync(id, input.PlaceId, input.Rating, input.Title, input.LikeCount, input.Status, input.Comment, input.UserId, input.ConcurrencyStamp);
        return ObjectMapper.Map<PlaceReview, PlaceReviewDto>(placeReview);
    }

    [AllowAnonymous]
    public virtual async Task<IRemoteStreamContent> GetListAsExcelFileAsync(PlaceReviewExcelDownloadDto input)
    {
        var downloadToken = await _downloadTokenCache.GetAsync(input.DownloadToken);
        if (downloadToken == null || input.DownloadToken != downloadToken.Token)
        {
            throw new AbpAuthorizationException("Invalid download token: " + input.DownloadToken);
        }

        var placeReviews = await _placeReviewRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.RatingMin, input.RatingMax, input.Title, input.Comment, input.LikeCountMin, input.LikeCountMax, input.Status, input.UserId, input.PlaceId);
        var items = placeReviews.Select(item => new { Rating = item.PlaceReview.Rating, Title = item.PlaceReview.Title, Comment = item.PlaceReview.Comment, LikeCount = item.PlaceReview.LikeCount, Status = item.PlaceReview.Status, UserId = item.PlaceReview.UserId, Place = item.Place?.Name, });
        var memoryStream = new MemoryStream();
        await memoryStream.SaveAsAsync(items);
        memoryStream.Seek(0, SeekOrigin.Begin);
        return new RemoteStreamContent(memoryStream, "PlaceReviews.xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
    }

    [Authorize(MasterDataServicePermissions.PlaceReviews.Delete)]
    public virtual async Task DeleteByIdsAsync(List<Guid> placereviewIds)
    {
        await _placeReviewRepository.DeleteManyAsync(placereviewIds);
    }

    [Authorize(MasterDataServicePermissions.PlaceReviews.Delete)]
    public virtual async Task DeleteAllAsync(GetPlaceReviewsInput input)
    {
        await _placeReviewRepository.DeleteAllAsync(input.FilterText, input.RatingMin, input.RatingMax, input.Title, input.Comment, input.LikeCountMin, input.LikeCountMax, input.Status, input.UserId, input.PlaceId);
    }

    public virtual async Task<KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto> GetDownloadTokenAsync()
    {
        var token = Guid.NewGuid().ToString("N");
        await _downloadTokenCache.SetAsync(token, new PlaceReviewDownloadTokenCacheItem { Token = token }, new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30) });
        return new KHHub.MasterDataService.Services.Dtos.Shared.DownloadTokenResultDto
        {
            Token = token
        };
    }
}