using System;

namespace KHHub.CrawlerSerivce.Crawling.Models;

/// <summary>
/// Internal model for one job row parsed from a listing page.
/// </summary>
public class CrawledJobListItem
{
    public string SourceAbsoluteUrl { get; set; } = null!;

    public string? ExternalJobId { get; set; }

    public string Title { get; set; } = null!;

    public string? CompanyName { get; set; }

    public string? EmploymentTypeLabel { get; set; }

    public string? SalaryDisplayText { get; set; }

    public DateTime? PublishedAt { get; set; }

    public DateTime? ApplicationDeadline { get; set; }
}
