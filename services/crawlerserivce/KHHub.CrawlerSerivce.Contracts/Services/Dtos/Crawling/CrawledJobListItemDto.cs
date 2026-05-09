using System;

namespace KHHub.CrawlerSerivce.Services.Dtos.Crawling;

/// <summary>
/// Normalized job row extracted from a listing page (before MasterData import).
/// </summary>
public class CrawledJobListItemDto
{
    public string SiteKey { get; set; } = null!;

    public string SourceAbsoluteUrl { get; set; } = null!;

    /// <summary>
    /// Stable id from source URL when available (e.g. trailing numeric id in slug).
    /// </summary>
    public string? ExternalJobId { get; set; }

    public string Title { get; set; } = null!;

    public string? CompanyName { get; set; }

    /// <summary>
    /// Raw label from the site (not mapped to MasterData enum yet).
    /// </summary>
    public string? EmploymentTypeLabel { get; set; }

    public string? SalaryDisplayText { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime? ApplicationDeadline { get; set; }
}
