using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Entities.Places;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.Places;

public abstract class PlaceUpdateDtoBase : IHasConcurrencyStamp
{
    [Required]
    [StringLength(PlaceConsts.NameMaxLength)]
    public string Name { get; set; } = null!;
    [Required]
    [StringLength(PlaceConsts.SlugMaxLength)]
    public string Slug { get; set; } = null!;
    [StringLength(PlaceConsts.ShortDescriptionMaxLength)]
    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    [StringLength(PlaceConsts.ThumbnailUrlMaxLength)]
    public string? ThumbnailUrl { get; set; }

    [StringLength(PlaceConsts.CoverImageUrlMaxLength)]
    public string? CoverImageUrl { get; set; }

    [StringLength(PlaceConsts.AddressMaxLength)]
    public string? Address { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longituded { get; set; }

    [StringLength(PlaceConsts.PhoneNumberMaxLength)]
    public string? PhoneNumber { get; set; }

    [StringLength(PlaceConsts.EmailMaxLength)]
    public string? Email { get; set; }

    [StringLength(PlaceConsts.WebsiteMaxLength)]
    public string? Website { get; set; }

    [StringLength(PlaceConsts.OpeningHoursMaxLength)]
    public string? OpeningHours { get; set; }

    public PriceRange PriceRange { get; set; }

    [StringLength(PlaceConsts.GoogleMapUrlMaxLength)]
    public string? GoogleMapUrl { get; set; }

    public PlaceStatus Status { get; set; }

    public int ViewCount { get; set; }

    public int FavoriteCount { get; set; }

    public int ReviewCount { get; set; }

    public decimal RatingAveraged { get; set; }

    public int RatingTotal { get; set; }

    public bool IsFeatured { get; set; }

    public bool IsHot { get; set; }

    public bool IsVerified { get; set; }

    [Required]
    [StringLength(PlaceConsts.SeoTitleMaxLength)]
    public string SeoTitle { get; set; } = null!;
    [StringLength(PlaceConsts.SeoDescriptionMaxLength)]
    public string? SeoDescription { get; set; }

    [StringLength(PlaceConsts.SeoKeywordsMaxLength)]
    public string? SeoKeywords { get; set; }

    public Guid PlaceCategoryId { get; set; }

    public Guid ProvinceId { get; set; }

    public Guid WardId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}