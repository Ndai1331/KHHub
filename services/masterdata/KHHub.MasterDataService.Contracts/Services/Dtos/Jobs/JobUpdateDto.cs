using KHHub.MasterDataService.Entities.Jobs;
using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.Jobs;

public abstract class JobUpdateDtoBase : IHasConcurrencyStamp
{
    [Required]
    [StringLength(JobConsts.TitleMaxLength)]
    public string Title { get; set; } = null!;
    [Required]
    [StringLength(JobConsts.SlugMaxLength)]
    public string Slug { get; set; } = null!;
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

    [EmailAddress]
    [StringLength(JobConsts.ContactEmailMaxLength)]
    public string? ContactEmail { get; set; }

    [StringLength(JobConsts.ContactPhoneMaxLength)]
    public string? ContactPhone { get; set; }

    [StringLength(JobConsts.ApplicationUrlMaxLength)]
    public string? ApplicationUrl { get; set; }

    public DateTime? PublishedAt { get; set; }

    public JobStatus Status { get; set; }

    public int ViewCount { get; set; }

    public int ApplicationCount { get; set; }

    public int FavoriteCount { get; set; }

    public int ShareCount { get; set; }

    public bool IsFeatured { get; set; }

    public bool IsUrgent { get; set; }

    public bool IsHot { get; set; }

    [Required]
    [StringLength(JobConsts.SeoTitleMaxLength)]
    public string SeoTitle { get; set; } = null!;
    [StringLength(JobConsts.SeoDescriptionMaxLength)]
    public string? SeoDescription { get; set; }

    [StringLength(JobConsts.SeoKeywordsMaxLength)]
    public string? SeoKeywords { get; set; }

    public Guid ProvinceId { get; set; }

    public Guid WardId { get; set; }

    public Guid JobCategoryId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}