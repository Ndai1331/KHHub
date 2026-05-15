using System;

namespace KHHub.CrawlerSerivce.Crawling.Models;

public class CrawledArticleListItem
{
    public string SourceAbsoluteUrl { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string? ThumbnailUrl { get; set; }

    public DateTime? PublishedAt { get; set; }

    public bool IsHot { get; set; }

    public bool IsTrending { get; set; }

    public string? CategoryLabel { get; set; }
}
