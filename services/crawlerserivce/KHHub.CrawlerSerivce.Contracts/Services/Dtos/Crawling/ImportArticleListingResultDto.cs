using System.Collections.Generic;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

public class ImportArticleListingResultDto
{
    public string SiteKey { get; set; } = null!;

    public int ListingRowsSeen { get; set; }

    public int ArticlesProcessed { get; set; }

    public int ArticlesCreated { get; set; }

    public int ArticlesUpdated { get; set; }

    public List<string> Warnings { get; set; } = new();

    public List<string> Errors { get; set; } = new();
}
