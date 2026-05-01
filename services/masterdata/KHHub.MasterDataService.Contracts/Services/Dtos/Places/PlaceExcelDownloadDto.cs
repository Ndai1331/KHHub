using KHHub.MasterDataService.Entities.Places;
using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.Places;

public abstract class PlaceExcelDownloadDtoBase
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }

    public string? Name { get; set; }

    public string? Slug { get; set; }

    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? CoverImageUrl { get; set; }

    public string? Address { get; set; }

    public decimal? LatitudeMin { get; set; }

    public decimal? LatitudeMax { get; set; }

    public decimal? LongitudedMin { get; set; }

    public decimal? LongitudedMax { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public string? OpeningHours { get; set; }

    public PriceRange? PriceRange { get; set; }

    public string? GoogleMapUrl { get; set; }

    public PlaceStatus? Status { get; set; }

    public int? ViewCountMin { get; set; }

    public int? ViewCountMax { get; set; }

    public int? FavoriteCountMin { get; set; }

    public int? FavoriteCountMax { get; set; }

    public int? ReviewCountMin { get; set; }

    public int? ReviewCountMax { get; set; }

    public decimal? RatingAveragedMin { get; set; }

    public decimal? RatingAveragedMax { get; set; }

    public int? RatingTotalMin { get; set; }

    public int? RatingTotalMax { get; set; }

    public bool? IsFeatured { get; set; }

    public bool? IsHot { get; set; }

    public bool? IsVerified { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public string? SeoKeywords { get; set; }

    public Guid? PlaceCategoryId { get; set; }

    public Guid? ProvinceId { get; set; }

    public Guid? WardId { get; set; }

    public PlaceExcelDownloadDtoBase()
    {
    }
}