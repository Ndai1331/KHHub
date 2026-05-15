using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KHHub.MasterDataService.Entities.Places;

namespace KHHub.MasterDataService.Services.Dtos.PlaceCrawler;

/// <summary>
/// Payload from place directory crawler after listing + detail normalization (machine-to-machine).
/// </summary>
public class PlaceCrawlerUpsertInputDto
{
    [Required]
    [StringLength(PlaceConsts.SourceUrlMaxLength)]
    public string SourceUrl { get; set; } = null!;

    [Required]
    [StringLength(PlaceConsts.NameMaxLength)]
    public string Name { get; set; } = null!;

    [StringLength(PlaceConsts.ShortDescriptionMaxLength)]
    public string? ShortDescription { get; set; }

    public string? Description { get; set; }

    [StringLength(PlaceConsts.ThumbnailUrlMaxLength)]
    public string? ThumbnailUrl { get; set; }

    [StringLength(PlaceConsts.CoverImageUrlMaxLength)]
    public string? CoverImageUrl { get; set; }

    [StringLength(PlaceConsts.AddressMaxLength)]
    public string? Address { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longituded { get; set; }

    [StringLength(PlaceConsts.PhoneNumberMaxLength)]
    public string? PhoneNumber { get; set; }

    [StringLength(PlaceConsts.EmailMaxLength)]
    public string? Email { get; set; }

    [StringLength(PlaceConsts.WebsiteMaxLength)]
    public string? Website { get; set; }

    [StringLength(PlaceConsts.OpeningHoursMaxLength)]
    public string? OpeningHours { get; set; }

    public PriceRange PriceRange { get; set; } = PriceRange.Free;

    [StringLength(PlaceConsts.GoogleMapUrlMaxLength)]
    public string? GoogleMapUrl { get; set; }

    [StringLength(PlaceConsts.SourceMaxLength)]
    public string? CrawlerSourceLabel { get; set; }

    public PlaceStatus Status { get; set; } = PlaceStatus.Draft;

    public decimal? RatingAveraged { get; set; }

    public int? ReviewCount { get; set; }

    public int? RatingTotal { get; set; }

    public bool IsFeatured { get; set; }

    public bool IsHot { get; set; }

    public bool IsVerified { get; set; }

    [Required]
    [StringLength(PlaceConsts.SeoTitleMaxLength)]
    public string SeoTitle { get; set; } = null!;

    public string? SeoDescription { get; set; }

    public string? SeoKeywords { get; set; }

    public Guid ProvinceId { get; set; }

    public Guid WardId { get; set; }

    public Guid PlaceCategoryId { get; set; }

    /// <summary>
    /// Resolved before fallback <see cref="PlaceCategoryId"/> (find-or-create active category).
    /// </summary>
    public string? PrimaryPlaceCategoryName { get; set; }

    public List<string> PlaceCategoryNameCandidates { get; set; } = new();

    /// <summary>
    /// Ordered hints for ward resolution inside <see cref="ProvinceId"/> (most specific first). Empty = use <see cref="WardId"/>.
    /// </summary>
    public List<string> WardNameCandidates { get; set; } = new();

    /// <summary>
    /// Tag display names; service creates tags and mappings as needed.
    /// </summary>
    public List<string> TagNames { get; set; } = new();
}
