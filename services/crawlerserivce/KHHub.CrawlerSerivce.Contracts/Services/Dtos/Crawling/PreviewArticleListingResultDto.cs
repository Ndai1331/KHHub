using System.Collections.Generic;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

public class PreviewArticleListingResultDto
{
    public string SiteKey { get; set; } = null!;

    public List<CrawledArticleListItemDto> Items { get; set; } = new();

    public List<string> Warnings { get; set; } = new();
}
