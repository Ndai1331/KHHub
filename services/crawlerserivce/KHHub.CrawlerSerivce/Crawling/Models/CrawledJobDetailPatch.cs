using System;
using System.Collections.Generic;

namespace KHHub.CrawlerSerivce.Crawling.Models;

/// <summary>
/// Extra fields parsed from a job detail page (merged with <see cref="CrawledJobListItem"/>).
/// </summary>
public class CrawledJobDetailPatch
{
    /// <summary>
    /// When set (e.g. from JSON-LD <c>datePosted</c>), overrides listing row for MasterData <c>PublishedAt</c>.
    /// </summary>
    public DateTime? PublishedAt { get; set; }

    public string? Summary { get; set; }

    public string? Description { get; set; }

    /// <summary>
    /// HTML from the "Mô tả công việc" block when present (preferred over schema.org description).
    /// </summary>
    public string? JobDescriptionHtml { get; set; }

    /// <summary>
    /// HTML from "Yêu cầu công việc".
    /// </summary>
    public string? RequirementsHtml { get; set; }

    /// <summary>
    /// HTML from "Quyền lợi được hưởng".
    /// </summary>
    public string? BenefitsHtml { get; set; }

    public decimal? SalaryMin { get; set; }

    public decimal? SalaryMax { get; set; }

    public string? SalaryCurrency { get; set; }

    public string? ThumbnailUrl { get; set; }

    /// <summary>
    /// Company/job logo image used as cover when the source exposes it (e.g. job-logo img).
    /// </summary>
    public string? CoverImageUrl { get; set; }

    public string? LocationDetail { get; set; }

    /// <summary>
    /// Badge texts under "Địa điểm tuyển dụng" (source order).
    /// </summary>
    public List<string> RecruitmentLocationLabels { get; set; } = new();

    /// <summary>
    /// Badge texts under "Ngành nghề".
    /// </summary>
    public List<string> IndustryLabels { get; set; } = new();

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    /// <summary>
    /// Parsed from "Thông tin liên hệ" (Địa chỉ line).
    /// </summary>
    public string? ContactAddress { get; set; }

    /// <summary>
    /// Raw employment label from source (e.g. schema.org or UI).
    /// </summary>
    public string? EmploymentTypeLabel { get; set; }
}
