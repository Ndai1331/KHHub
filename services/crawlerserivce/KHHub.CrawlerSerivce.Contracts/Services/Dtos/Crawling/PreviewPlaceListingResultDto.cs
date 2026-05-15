using System.Collections.Generic;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

public class PreviewPlaceListingResultDto
{
    public string SiteKey { get; set; } = null!;

    public List<CrawledPlaceListItemDto> Items { get; set; } = new();

    public List<string> Warnings { get; set; } = new();
}
