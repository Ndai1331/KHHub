using KHHub.MasterDataService.Entities.Jobs;
using Volo.Abp.Application.Dtos;
using System;

namespace KHHub.MasterDataService.Services.Dtos.Jobs;

public abstract class JobExcelDownloadDtoBase
{
    public string DownloadToken { get; set; } = null!;
    public string? FilterText { get; set; }

    public string? Title { get; set; }

    public string? Slug { get; set; }

    public string? Summary { get; set; }

    public string? Description { get; set; }

    public string? Requirements { get; set; }

    public string? Benefits { get; set; }

    public string? ThumbnailUrl { get; set; }

    public string? CoverImageUrl { get; set; }

    public EmploymentType? EmploymentType { get; set; }

    public WorkMode? WorkMode { get; set; }

    public ExperienceLevel? ExperienceLevel { get; set; }

    public decimal? SalaryMinMin { get; set; }

    public decimal? SalaryMinMax { get; set; }

    public decimal? SalaryMaxMin { get; set; }

    public decimal? SalaryMaxMax { get; set; }

    public string? SalaryText { get; set; }

    public string? SalaryCurrency { get; set; }

    public string? Location { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? ApplicationUrl { get; set; }

    public DateTime? PublishedAtMin { get; set; }

    public DateTime? PublishedAtMax { get; set; }

    public JobStatus? Status { get; set; }

    public int? ViewCountMin { get; set; }

    public int? ViewCountMax { get; set; }

    public int? ApplicationCountMin { get; set; }

    public int? ApplicationCountMax { get; set; }

    public int? FavoriteCountMin { get; set; }

    public int? FavoriteCountMax { get; set; }

    public int? ShareCountMin { get; set; }

    public int? ShareCountMax { get; set; }

    public bool? IsFeatured { get; set; }

    public bool? IsUrgent { get; set; }

    public bool? IsHot { get; set; }

    public string? SeoTitle { get; set; }

    public string? SeoDescription { get; set; }

    public string? SeoKeywords { get; set; }

    public Guid? ProvinceId { get; set; }

    public Guid? WardId { get; set; }

    public Guid? JobCategoryId { get; set; }

    public JobExcelDownloadDtoBase()
    {
    }
}