using KHHub.MasterDataService.Entities.Places;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.Places;

namespace KHHub.MasterDataService.Entities.Places;

public abstract class PlaceManagerBase : DomainService
{
    protected IPlaceRepository _placeRepository;

    public PlaceManagerBase(IPlaceRepository placeRepository)
    {
        _placeRepository = placeRepository;
    }

    public virtual async Task<Place> CreateAsync(Guid placeCategoryId, Guid provinceId, Guid wardId, string name, string slug, decimal latitude, decimal longituded, PriceRange priceRange, PlaceStatus status, int viewCount, int favoriteCount, int reviewCount, decimal ratingAveraged, int ratingTotal, bool isFeatured, bool isHot, bool isVerified, string seoTitle, string? shortDescription = null, string? description = null, string? thumbnailUrl = null, string? coverImageUrl = null, string? address = null, string? phoneNumber = null, string? email = null, string? website = null, string? openingHours = null, string? googleMapUrl = null, string? seoDescription = null, string? seoKeywords = null)
    {
        Check.NotNull(placeCategoryId, nameof(placeCategoryId));
        Check.NotNull(provinceId, nameof(provinceId));
        Check.NotNull(wardId, nameof(wardId));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), PlaceConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), PlaceConsts.SlugMaxLength);
        Check.NotNull(priceRange, nameof(priceRange));
        Check.NotNull(status, nameof(status));
        Check.NotNullOrWhiteSpace(seoTitle, nameof(seoTitle));
        Check.Length(seoTitle, nameof(seoTitle), PlaceConsts.SeoTitleMaxLength);
        Check.Length(shortDescription, nameof(shortDescription), PlaceConsts.ShortDescriptionMaxLength);
        Check.Length(thumbnailUrl, nameof(thumbnailUrl), PlaceConsts.ThumbnailUrlMaxLength);
        Check.Length(coverImageUrl, nameof(coverImageUrl), PlaceConsts.CoverImageUrlMaxLength);
        Check.Length(address, nameof(address), PlaceConsts.AddressMaxLength);
        Check.Length(phoneNumber, nameof(phoneNumber), PlaceConsts.PhoneNumberMaxLength);
        Check.Length(email, nameof(email), PlaceConsts.EmailMaxLength);
        Check.Length(website, nameof(website), PlaceConsts.WebsiteMaxLength);
        Check.Length(openingHours, nameof(openingHours), PlaceConsts.OpeningHoursMaxLength);
        Check.Length(googleMapUrl, nameof(googleMapUrl), PlaceConsts.GoogleMapUrlMaxLength);
        Check.Length(seoDescription, nameof(seoDescription), PlaceConsts.SeoDescriptionMaxLength);
        Check.Length(seoKeywords, nameof(seoKeywords), PlaceConsts.SeoKeywordsMaxLength);
        var place = new Place(GuidGenerator.Create(), placeCategoryId, provinceId, wardId, name, slug, latitude, longituded, priceRange, status, viewCount, favoriteCount, reviewCount, ratingAveraged, ratingTotal, isFeatured, isHot, isVerified, seoTitle, shortDescription, description, thumbnailUrl, coverImageUrl, address, phoneNumber, email, website, openingHours, googleMapUrl, seoDescription, seoKeywords);
        return await _placeRepository.InsertAsync(place);
    }

    public virtual async Task<Place> UpdateAsync(Guid id, Guid placeCategoryId, Guid provinceId, Guid wardId, string name, string slug, decimal latitude, decimal longituded, PriceRange priceRange, PlaceStatus status, int viewCount, int favoriteCount, int reviewCount, decimal ratingAveraged, int ratingTotal, bool isFeatured, bool isHot, bool isVerified, string seoTitle, string? shortDescription = null, string? description = null, string? thumbnailUrl = null, string? coverImageUrl = null, string? address = null, string? phoneNumber = null, string? email = null, string? website = null, string? openingHours = null, string? googleMapUrl = null, string? seoDescription = null, string? seoKeywords = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(placeCategoryId, nameof(placeCategoryId));
        Check.NotNull(provinceId, nameof(provinceId));
        Check.NotNull(wardId, nameof(wardId));
        Check.NotNullOrWhiteSpace(name, nameof(name));
        Check.Length(name, nameof(name), PlaceConsts.NameMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), PlaceConsts.SlugMaxLength);
        Check.NotNull(priceRange, nameof(priceRange));
        Check.NotNull(status, nameof(status));
        Check.NotNullOrWhiteSpace(seoTitle, nameof(seoTitle));
        Check.Length(seoTitle, nameof(seoTitle), PlaceConsts.SeoTitleMaxLength);
        Check.Length(shortDescription, nameof(shortDescription), PlaceConsts.ShortDescriptionMaxLength);
        Check.Length(thumbnailUrl, nameof(thumbnailUrl), PlaceConsts.ThumbnailUrlMaxLength);
        Check.Length(coverImageUrl, nameof(coverImageUrl), PlaceConsts.CoverImageUrlMaxLength);
        Check.Length(address, nameof(address), PlaceConsts.AddressMaxLength);
        Check.Length(phoneNumber, nameof(phoneNumber), PlaceConsts.PhoneNumberMaxLength);
        Check.Length(email, nameof(email), PlaceConsts.EmailMaxLength);
        Check.Length(website, nameof(website), PlaceConsts.WebsiteMaxLength);
        Check.Length(openingHours, nameof(openingHours), PlaceConsts.OpeningHoursMaxLength);
        Check.Length(googleMapUrl, nameof(googleMapUrl), PlaceConsts.GoogleMapUrlMaxLength);
        Check.Length(seoDescription, nameof(seoDescription), PlaceConsts.SeoDescriptionMaxLength);
        Check.Length(seoKeywords, nameof(seoKeywords), PlaceConsts.SeoKeywordsMaxLength);
        var place = await _placeRepository.GetAsync(id);
        place.PlaceCategoryId = placeCategoryId;
        place.ProvinceId = provinceId;
        place.WardId = wardId;
        place.Name = name;
        place.Slug = slug;
        place.Latitude = latitude;
        place.Longituded = longituded;
        place.PriceRange = priceRange;
        place.Status = status;
        place.ViewCount = viewCount;
        place.FavoriteCount = favoriteCount;
        place.ReviewCount = reviewCount;
        place.RatingAveraged = ratingAveraged;
        place.RatingTotal = ratingTotal;
        place.IsFeatured = isFeatured;
        place.IsHot = isHot;
        place.IsVerified = isVerified;
        place.SeoTitle = seoTitle;
        place.ShortDescription = shortDescription;
        place.Description = description;
        place.ThumbnailUrl = thumbnailUrl;
        place.CoverImageUrl = coverImageUrl;
        place.Address = address;
        place.PhoneNumber = phoneNumber;
        place.Email = email;
        place.Website = website;
        place.OpeningHours = openingHours;
        place.GoogleMapUrl = googleMapUrl;
        place.SeoDescription = seoDescription;
        place.SeoKeywords = seoKeywords;
        place.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _placeRepository.UpdateAsync(place);
    }
}