using System;

namespace KHHub.CrawlerSerivce.Crawling.Models;

public class CrawledPlaceListItem
{
    public string SourceAbsoluteUrl { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public string? ThumbnailUrl { get; set; }

    public decimal? Rating { get; set; }

    public string? AddressSnippet { get; set; }
}
