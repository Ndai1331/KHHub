using System;
using System.Collections.Generic;

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

    /// <summary>
    /// Parent category hints from listing URL (immediate parent first).
    /// </summary>
    public List<string> ArticleCategoryNameCandidates { get; set; } = new();
}
