using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using KHHub.MasterDataService.Entities.Jobs;

namespace KHHub.MasterDataService.Services.Dtos.JobCrawler;

/// <summary>
/// Payload from crawler after listing + detail normalization (machine-to-machine).
/// </summary>
public class JobCrawlerUpsertInputDto
{
    [Required]
    [StringLength(JobConsts.ApplicationUrlMaxLength)]
    public string ApplicationUrl { get; set; } = null!;

    [Required]
    [StringLength(JobConsts.TitleMaxLength)]
    public string Title { get; set; } = null!;

    [StringLength(JobConsts.SummaryMaxLength)]
    public string? Summary { get; set; }

    public string? Description { get; set; }

    public string? Requirements { get; set; }

    public string? Benefits { get; set; }

    [StringLength(JobConsts.ThumbnailUrlMaxLength)]
    public string? ThumbnailUrl { get; set; }

    [StringLength(JobConsts.CoverImageUrlMaxLength)]
    public string? CoverImageUrl { get; set; }

    public EmploymentType? EmploymentType { get; set; }

    public WorkMode? WorkMode { get; set; }

    public ExperienceLevel? ExperienceLevel { get; set; }

    public decimal? SalaryMin { get; set; }

    public decimal? SalaryMax { get; set; }

    [StringLength(JobConsts.SalaryTextMaxLength)]
    public string? SalaryText { get; set; }

    [StringLength(JobConsts.SalaryCurrencyMaxLength)]
    public string? SalaryCurrency { get; set; }

    [StringLength(JobConsts.LocationMaxLength)]
    public string? Location { get; set; }

    [StringLength(JobConsts.ContactEmailMaxLength)]
    public string? ContactEmail { get; set; }

    [StringLength(JobConsts.ContactPhoneMaxLength)]
    public string? ContactPhone { get; set; }

    public DateTime? PublishedAt { get; set; }

    public JobStatus Status { get; set; } = JobStatus.Published;

    public Guid ProvinceId { get; set; }

    public Guid WardId { get; set; }

    public Guid JobCategoryId { get; set; }

    /// <summary>
    /// Tag display names; upsert creates JobTag + JobTagMapping as needed.
    /// </summary>
    public List<string> TagNames { get; set; } = new();

    /// <summary>
    /// Ordered hints for ward resolution inside <see cref="ProvinceId"/> (most specific first). Empty = use <see cref="WardId"/>.
    /// </summary>
    public List<string> WardNameCandidates { get; set; } = new();

    /// <summary>
    /// Optional primary category display name; MasterData find-or-creates category when set. Falls back to <see cref="JobCategoryId"/>.
    /// </summary>
    public string? PrimaryJobCategoryName { get; set; }

    [StringLength(JobConsts.SeoDescriptionMaxLength)]
    public string? SeoDescription { get; set; }

    [StringLength(JobConsts.SeoKeywordsMaxLength)]
    public string? SeoKeywords { get; set; }
}
