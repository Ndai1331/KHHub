using System.Collections.Generic;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

public class ImportJobListingResultDto
{
    public string SiteKey { get; set; } = null!;

    public int ListingRowsSeen { get; set; }

    public int JobsProcessed { get; set; }

    public int JobsCreated { get; set; }

    public int JobsUpdated { get; set; }

    public List<string> Warnings { get; set; } = new();

    public List<string> Errors { get; set; } = new();
}
