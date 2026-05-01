using KHHub.MasterDataService.Entities.Places;
using System;

namespace KHHub.MasterDataService.Services.Dtos.Places;

public abstract class PlaceExcelDtoBase
{
    public string Name { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? CoverImageUrl { get; set; }

    public string? Address { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longituded { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Website { get; set; }

    public string? OpeningHours { get; set; }

    public PriceRange PriceRange { get; set; }

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

    public string SeoTitle { get; set; } = null!;
    public string? SeoDescription { get; set; }

    public string? SeoKeywords { get; set; }
}