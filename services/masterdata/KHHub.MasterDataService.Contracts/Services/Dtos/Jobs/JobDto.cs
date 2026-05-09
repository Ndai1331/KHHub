using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities;

namespace KHHub.MasterDataService.Services.Dtos.Jobs;

public abstract class JobDtoBase : FullAuditedEntityDto<Guid>, IHasConcurrencyStamp
{
    public string Title { get; set; } = null!;
    public string Slug { get; set; } = null!;
    public string? Summary { get; set; }

    public string? Description { get; set; }

    public string? Requirements { get; set; }

    public string? Benefits { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? CoverImageUrl { get; set; }

    public EmploymentType? EmploymentType { get; set; }

    public WorkMode? WorkMode { get; set; }

    public ExperienceLevel? ExperienceLevel { get; set; }

    public decimal? SalaryMin { get; set; }

    public decimal? SalaryMax { get; set; }

    public string? SalaryText { get; set; }

    public string? SalaryCurrency { get; set; }

    public string? Location { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

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

    public string SeoTitle { get; set; } = null!;
    public string? SeoDescription { get; set; }

    public string? SeoKeywords { get; set; }

    public Guid ProvinceId { get; set; }

    public Guid WardId { get; set; }

    public Guid JobCategoryId { get; set; }

    public string ConcurrencyStamp { get; set; } = null!;
}