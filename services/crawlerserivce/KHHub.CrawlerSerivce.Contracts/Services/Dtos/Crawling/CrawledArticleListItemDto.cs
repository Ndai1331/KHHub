using System;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

public class CrawledArticleListItemDto
{
    public string SiteKey { get; set; } = null!;

    public string SourceAbsoluteUrl { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? ThumbnailUrl { get; set; }

    public DateTime? PublishedAt { get; set; }
}
