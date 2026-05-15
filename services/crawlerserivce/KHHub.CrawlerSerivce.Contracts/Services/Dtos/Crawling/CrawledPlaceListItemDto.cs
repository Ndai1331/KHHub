using System;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

public class CrawledPlaceListItemDto
{
    public string SiteKey { get; set; } = null!;

    public string SourceAbsoluteUrl { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string? ShortDescription { get; set; }
}
