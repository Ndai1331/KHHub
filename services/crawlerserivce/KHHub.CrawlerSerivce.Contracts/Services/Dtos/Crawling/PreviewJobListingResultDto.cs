using System.Collections.Generic;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

public class PreviewJobListingResultDto
{
    public string SiteKey { get; set; } = null!;

    public List<CrawledJobListItemDto> Items { get; set; } = new();

    /// <summary>
    /// Non-fatal issues (e.g. skipped malformed rows).
    /// </summary>
    public List<string> Warnings { get; set; } = new();
}
