using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

public class ImportArticleListingInput
{
    [Required]
    [StringLength(2048)]
    public string ListingUrl { get; set; } = null!;

    [Range(1, 1000)]
    public int MaxPages { get; set; } = 1;

    [Range(1, 500)]
    public int? MaxArticlesToImport { get; set; }

    [Range(0, 60_000)]
    public int DelayBetweenDetailRequestsMs { get; set; } = 800;

    /// <summary>
    /// Optional; empty GUID uses <c>Crawler:ArticleImportDefaults:ArticleCategoryId</c>.
    /// </summary>
    public Guid? ArticleCategoryId { get; set; }

    public List<string> ExtraTagNames { get; set; } = new();
}
