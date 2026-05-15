using System;
using System.Collections.Generic;

namespace KHHub.CrawlerSerivce.Crawling.Models;

public class CrawledArticleDetailPatch
{
    public string? Summary { get; set; }

    public string? ContentHtml { get; set; }

    public string? AuthorName { get; set; }

    public string? SourceLabel { get; set; }

    public DateTime? PublishedAt { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? CoverImageUrl { get; set; }

    public string? CategoryLabel { get; set; }

    public string? SeoDescription { get; set; }

    /// <summary>
    /// Extra category name hints for MasterData resolve (parents / breadcrumb); most specific stays in <see cref="CategoryLabel"/>.
    /// </summary>
    public List<string> ArticleCategoryNameCandidates { get; set; } = new();

    public List<string> TopicLabels { get; set; } = new();
}
