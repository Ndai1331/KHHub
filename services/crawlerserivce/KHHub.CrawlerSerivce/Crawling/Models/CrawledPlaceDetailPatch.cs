using System;
using System.Collections.Generic;

namespace KHHub.CrawlerSerivce.Crawling.Models;

public class CrawledPlaceDetailPatch
{
    public string? Description { get; set; }

    public string? ShortDescription { get; set; }

    public string? Address { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longituded { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Website { get; set; }

    public string? OpeningHours { get; set; }

    public string? GoogleMapUrl { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? CoverImageUrl { get; set; }

    public decimal? RatingAveraged { get; set; }

    public int? ReviewCount { get; set; }

    public string? PriceRangeLabel { get; set; }

    public string? CategoryLabel { get; set; }

    public IReadOnlyList<string> WardNameHints { get; set; } = Array.Empty<string>();
}
