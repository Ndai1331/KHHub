using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Wards;
using KHHub.MasterDataService.Entities.Provinces;
using KHHub.MasterDataService.Entities.PlaceCategories;
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
using KHHub.MasterDataService.Services.Places;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.MasterDataService.Data.Places;
using KHHub.MasterDataService.Services.Dtos.Shared;

namespace KHHub.MasterDataService.Services.Places;

[Authorize(MasterDataServicePermissions.Places.Default)]
public abstract class PlacesAppServiceBase : ApplicationService
{
    protected IPlaceRepository _placeRepository;
    protected PlaceManager _placeManager;
    protected IRepository<KHHub.MasterDataService.Entities.PlaceCategories.PlaceCategory, Guid> _placeCategoryRepository;
    protected IRepository<KHHub.MasterDataService.Entities.Provinces.Province, Guid> _provinceRepository;
    protected IRepository<KHHub.MasterDataService.Entities.Wards.Ward, Guid> _wardRepository;

    public PlacesAppServiceBase(IPlaceRepository placeRepository, PlaceManager placeManager, IRepository<KHHub.MasterDataService.Entities.PlaceCategories.PlaceCategory, Guid> placeCategoryRepository, IRepository<KHHub.MasterDataService.Entities.Provinces.Province, Guid> provinceRepository, IRepository<KHHub.MasterDataService.Entities.Wards.Ward, Guid> wardRepository)
    {
        _placeRepository = placeRepository;
        _placeManager = placeManager;
        _placeCategoryRepository = placeCategoryRepository;
        _provinceRepository = provinceRepository;
        _wardRepository = wardRepository;
    }

    public virtual async Task<PagedResultDto<PlaceWithNavigationPropertiesDto>> GetListAsync(GetPlacesInput input)
    {
        var totalCount = await _placeRepository.GetCountAsync(input.FilterText, input.Name, input.Slug, input.ShortDescription, input.Description, input.ThumbnailUrl, input.CoverImageUrl, input.Address, input.LatitudeMin, input.LatitudeMax, input.LongitudedMin, input.LongitudedMax, input.PhoneNumber, input.Email, input.Website, input.OpeningHours, input.PriceRange, input.GoogleMapUrl, input.Status, input.ViewCountMin, input.ViewCountMax, input.FavoriteCountMin, input.FavoriteCountMax, input.ReviewCountMin, input.ReviewCountMax, input.RatingAveragedMin, input.RatingAveragedMax, input.RatingTotalMin, input.RatingTotalMax, input.IsFeatured, input.IsHot, input.IsVerified, input.SeoTitle, input.SeoDescription, input.SeoKeywords, input.Source, input.SourceUrl, input.PlaceCategoryId, input.ProvinceId, input.WardId);
        var items = await _placeRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Name, input.Slug, input.ShortDescription, input.Description, input.ThumbnailUrl, input.CoverImageUrl, input.Address, input.LatitudeMin, input.LatitudeMax, input.LongitudedMin, input.LongitudedMax, input.PhoneNumber, input.Email, input.Website, input.OpeningHours, input.PriceRange, input.GoogleMapUrl, input.Status, input.ViewCountMin, input.ViewCountMax, input.FavoriteCountMin, input.FavoriteCountMax, input.ReviewCountMin, input.ReviewCountMax, input.RatingAveragedMin, input.RatingAveragedMax, input.RatingTotalMin, input.RatingTotalMax, input.IsFeatured, input.IsHot, input.IsVerified, input.SeoTitle, input.SeoDescription, input.SeoKeywords, input.Source, input.SourceUrl, input.PlaceCategoryId, input.ProvinceId, input.WardId, input.Sorting, input.MaxResultCount, input.SkipCount);
        return new PagedResultDto<PlaceWithNavigationPropertiesDto>
        {
            TotalCount = totalCount,
            Items = ObjectMapper.Map<List<PlaceWithNavigationProperties>, List<PlaceWithNavigationPropertiesDto>>(items)
        };
    }

    public virtual async Task<PlaceWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
    {
        return ObjectMapper.Map<PlaceWithNavigationProperties, PlaceWithNavigationPropertiesDto>(await _placeRepository.GetWithNavigationPropertiesAsync(id));
    }

    public virtual async Task<PlaceDto> GetAsync(Guid id)
    {
        return ObjectMapper.Map<Place, PlaceDto>(await _placeRepository.GetAsync(id));
    }

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetPlaceCategoryLookupAsync(LookupRequestDto input)
    {
        var query = (await _placeCategoryRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name != null && x.Name.Contains(input.Filter));
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.PlaceCategories.PlaceCategory>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.Name }).ToList()
        };
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

    public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetWardLookupAsync(LookupRequestDto input)
    {
        var query = (await _wardRepository.GetQueryableAsync()).WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name != null && x.Name.Contains(input.Filter));
        var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<KHHub.MasterDataService.Entities.Wards.Ward>();
        var totalCount = query.Count();
        return new PagedResultDto<LookupDto<Guid>>
        {
            TotalCount = totalCount,
            Items = lookupData.Select(x => new LookupDto<Guid> { Id = x.Id, DisplayName = x.Name }).ToList()
        };
    }

    [Authorize(MasterDataServicePermissions.Places.Delete)]
    public virtual async Task DeleteAsync(Guid id)
    {
        await _placeRepository.DeleteAsync(id);
    }

    [Authorize(MasterDataServicePermissions.Places.Create)]
    public virtual async Task<PlaceDto> CreateAsync(PlaceCreateDto input)
    {
        if (input.PlaceCategoryId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["PlaceCategory"]]);
        }

        if (input.ProvinceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Province"]]);
        }

        if (input.WardId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Ward"]]);
        }

        var place = await _placeManager.CreateAsync(input.PlaceCategoryId, input.ProvinceId, input.WardId, input.Name, input.Slug, input.Latitude, input.Longituded, input.PriceRange, input.Status, input.ViewCount, input.FavoriteCount, input.ReviewCount, input.RatingAveraged, input.RatingTotal, input.IsFeatured, input.IsHot, input.IsVerified, input.SeoTitle, input.ShortDescription, input.Description, input.ThumbnailUrl, input.CoverImageUrl, input.Address, input.PhoneNumber, input.Email, input.Website, input.OpeningHours, input.GoogleMapUrl, input.SeoDescription, input.SeoKeywords, input.Source, input.SourceUrl);
        return ObjectMapper.Map<Place, PlaceDto>(place);
    }

    [Authorize(MasterDataServicePermissions.Places.Edit)]
    public virtual async Task<PlaceDto> UpdateAsync(Guid id, PlaceUpdateDto input)
    {
        if (input.PlaceCategoryId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["PlaceCategory"]]);
        }

        if (input.ProvinceId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Province"]]);
        }

        if (input.WardId == default)
        {
            throw new UserFriendlyException(L["The {0} field is required.", L["Ward"]]);
        }

        var place = await _placeManager.UpdateAsync(id, input.PlaceCategoryId, input.ProvinceId, input.WardId, input.Name, input.Slug, input.Latitude, input.Longituded, input.PriceRange, input.Status, input.ViewCount, input.FavoriteCount, input.ReviewCount, input.RatingAveraged, input.RatingTotal, input.IsFeatured, input.IsHot, input.IsVerified, input.SeoTitle, input.ShortDescription, input.Description, input.ThumbnailUrl, input.CoverImageUrl, input.Address, input.PhoneNumber, input.Email, input.Website, input.OpeningHours, input.GoogleMapUrl, input.SeoDescription, input.SeoKeywords, input.Source, input.SourceUrl, input.ConcurrencyStamp);
        return ObjectMapper.Map<Place, PlaceDto>(place);
    }
}