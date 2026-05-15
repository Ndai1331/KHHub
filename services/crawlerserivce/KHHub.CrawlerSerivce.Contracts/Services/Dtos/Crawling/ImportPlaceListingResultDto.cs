using System.Collections.Generic;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

public class ImportPlaceListingResultDto
{
    public string SiteKey { get; set; } = null!;

    public int ListingRowsSeen { get; set; }

    public int PlacesProcessed { get; set; }

    public int PlacesCreated { get; set; }

    public int PlacesUpdated { get; set; }

    public List<string> Warnings { get; set; } = new();

    public List<string> Errors { get; set; } = new();
}
