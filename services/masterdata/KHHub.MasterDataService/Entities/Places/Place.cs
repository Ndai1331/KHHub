using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Entities.PlaceCategories;
using KHHub.MasterDataService.Entities.Provinces;
using KHHub.MasterDataService.Entities.Wards;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.Places;

public abstract class PlaceBase : FullAuditedAggregateRoot<Guid>
{
    [NotNull]
    public virtual string Name { get; set; }

    [NotNull]
    public virtual string Slug { get; set; }

    [CanBeNull]
    public virtual string? ShortDescription { get; set; }

    [CanBeNull]
    public virtual string? Description { get; set; }

    [CanBeNull]
    public virtual string? ThumbnailUrl { get; set; }

    [CanBeNull]
    public virtual string? CoverImageUrl { get; set; }

    [CanBeNull]
    public virtual string? Address { get; set; }

    public virtual decimal Latitude { get; set; }

    public virtual decimal Longituded { get; set; }

    [CanBeNull]
    public virtual string? PhoneNumber { get; set; }

    [CanBeNull]
    public virtual string? Email { get; set; }

    [CanBeNull]
    public virtual string? Website { get; set; }

    [CanBeNull]
    public virtual string? OpeningHours { get; set; }

    public virtual PriceRange PriceRange { get; set; }

    [CanBeNull]
    public virtual string? GoogleMapUrl { get; set; }

    public virtual PlaceStatus Status { get; set; }

    public virtual int ViewCount { get; set; }

    public virtual int FavoriteCount { get; set; }

    public virtual int ReviewCount { get; set; }

    public virtual decimal RatingAveraged { get; set; }

    public virtual int RatingTotal { get; set; }

    public virtual bool IsFeatured { get; set; }

    public virtual bool IsHot { get; set; }

    public virtual bool IsVerified { get; set; }

    [NotNull]
    public virtual string SeoTitle { get; set; }

    [CanBeNull]
    public virtual string? SeoDescription { get; set; }

    [CanBeNull]
    public virtual string? SeoKeywords { get; set; }

    public Guid PlaceCategoryId { get; set; }

    public Guid ProvinceId { get; set; }

    public Guid WardId { get; set; }

    protected PlaceBase()
    {
    }

    public PlaceBase(Guid id, Guid placeCategoryId, Guid provinceId, Guid wardId, string name, string slug, decimal latitude, decimal longituded, PriceRange priceRange, PlaceStatus status, int viewCount, int favoriteCount, int reviewCount, decimal ratingAveraged, int ratingTotal, bool isFeatured, bool isHot, bool isVerified, string seoTitle, string? shortDescription = null, string? description = null, string? thumbnailUrl = null, string? coverImageUrl = null, string? address = null, string? phoneNumber = null, string? email = null, string? website = null, string? openingHours = null, string? googleMapUrl = null, string? seoDescription = null, string? seoKeywords = null)
    {
        Id = id;
        Check.NotNull(name, nameof(name));
        Check.Length(name, nameof(name), PlaceConsts.NameMaxLength, 0);
        Check.NotNull(slug, nameof(slug));
        Check.Length(slug, nameof(slug), PlaceConsts.SlugMaxLength, 0);
        Check.NotNull(seoTitle, nameof(seoTitle));
        Check.Length(seoTitle, nameof(seoTitle), PlaceConsts.SeoTitleMaxLength, 0);
        Check.Length(shortDescription, nameof(shortDescription), PlaceConsts.ShortDescriptionMaxLength, 0);
        Check.Length(thumbnailUrl, nameof(thumbnailUrl), PlaceConsts.ThumbnailUrlMaxLength, 0);
        Check.Length(coverImageUrl, nameof(coverImageUrl), PlaceConsts.CoverImageUrlMaxLength, 0);
        Check.Length(address, nameof(address), PlaceConsts.AddressMaxLength, 0);
        Check.Length(phoneNumber, nameof(phoneNumber), PlaceConsts.PhoneNumberMaxLength, 0);
        Check.Length(email, nameof(email), PlaceConsts.EmailMaxLength, 0);
        Check.Length(website, nameof(website), PlaceConsts.WebsiteMaxLength, 0);
        Check.Length(openingHours, nameof(openingHours), PlaceConsts.OpeningHoursMaxLength, 0);
        Check.Length(googleMapUrl, nameof(googleMapUrl), PlaceConsts.GoogleMapUrlMaxLength, 0);
        Check.Length(seoDescription, nameof(seoDescription), PlaceConsts.SeoDescriptionMaxLength, 0);
        Check.Length(seoKeywords, nameof(seoKeywords), PlaceConsts.SeoKeywordsMaxLength, 0);
        Name = name;
        Slug = slug;
        Latitude = latitude;
        Longituded = longituded;
        PriceRange = priceRange;
        Status = status;
        ViewCount = viewCount;
        FavoriteCount = favoriteCount;
        ReviewCount = reviewCount;
        RatingAveraged = ratingAveraged;
        RatingTotal = ratingTotal;
        IsFeatured = isFeatured;
        IsHot = isHot;
        IsVerified = isVerified;
        SeoTitle = seoTitle;
        ShortDescription = shortDescription;
        Description = description;
        ThumbnailUrl = thumbnailUrl;
        CoverImageUrl = coverImageUrl;
        Address = address;
        PhoneNumber = phoneNumber;
        Email = email;
        Website = website;
        OpeningHours = openingHours;
        GoogleMapUrl = googleMapUrl;
        SeoDescription = seoDescription;
        SeoKeywords = seoKeywords;
        PlaceCategoryId = placeCategoryId;
        ProvinceId = provinceId;
        WardId = wardId;
    }
}