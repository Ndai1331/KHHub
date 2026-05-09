using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

public class ImportJobListingInput
{
    [Required]
    [StringLength(2048)]
    public string ListingUrl { get; set; } = null!;

    /// <summary>
    /// Listing pages to fetch (page 1 … MaxPages). Site handlers map this to ?page=2, etc.
    /// Use with <see cref="MaxJobsToImport"/> null to process every distinct job link found on those pages.
    /// </summary>
    [Range(1, 1000)]
    public int MaxPages { get; set; } = 1;

    /// <summary>
    /// Cap distinct listing rows (jobs) queued for detail import; null = no cap (only bounded by <see cref="MaxPages"/>).
    /// </summary>
    [Range(1, 500)]
    public int? MaxJobsToImport { get; set; }

    [Range(0, 60_000)]
    public int DelayBetweenDetailRequestsMs { get; set; } = 400;

    /// <summary>
    /// Optional; when null or empty GUID, uses <c>Crawler:ImportDefaults:ProvinceId</c> from appsettings.
    /// </summary>
    public Guid? ProvinceId { get; set; }

    /// <summary>
    /// Optional; when null or empty GUID, uses <c>Crawler:ImportDefaults:WardId</c>.
    /// </summary>
    public Guid? WardId { get; set; }

    /// <summary>
    /// Optional; when null or empty GUID, uses <c>Crawler:ImportDefaults:JobCategoryId</c>.
    /// </summary>
    public Guid? JobCategoryId { get; set; }

    /// <summary>
    /// Additional tags stored as JobTag + JobTagMapping in MasterData.
    /// </summary>
    public List<string> ExtraTagNames { get; set; } = new();
}
