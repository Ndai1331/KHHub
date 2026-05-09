using KHHub.MasterDataService.Entities.Jobs;
using KHHub.MasterDataService.Entities.Provinces;
using KHHub.MasterDataService.Entities.Wards;
using KHHub.MasterDataService.Entities.JobCategories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;
using JetBrains.Annotations;
using Volo.Abp;

namespace KHHub.MasterDataService.Entities.Jobs;

public abstract class JobBase : FullAuditedAggregateRoot<Guid>
{
    [NotNull]
    public virtual string Title { get; set; }

    [NotNull]
    public virtual string Slug { get; set; }

    [CanBeNull]
    public virtual string? Summary { get; set; }

    [CanBeNull]
    public virtual string? Description { get; set; }

    [CanBeNull]
    public virtual string? Requirements { get; set; }

    [CanBeNull]
    public virtual string? Benefits { get; set; }

    [CanBeNull]
    public virtual string? ThumbnailUrl { get; set; }

    [CanBeNull]
    public virtual string? CoverImageUrl { get; set; }

    public virtual EmploymentType? EmploymentType { get; set; }

    public virtual WorkMode? WorkMode { get; set; }

    public virtual ExperienceLevel? ExperienceLevel { get; set; }

    public virtual decimal? SalaryMin { get; set; }

    public virtual decimal? SalaryMax { get; set; }

    [CanBeNull]
    public virtual string? SalaryText { get; set; }

    [CanBeNull]
    public virtual string? SalaryCurrency { get; set; }

    [CanBeNull]
    public virtual string? Location { get; set; }

    [CanBeNull]
    public virtual string? ContactEmail { get; set; }

    [CanBeNull]
    public virtual string? ContactPhone { get; set; }

    [CanBeNull]
    public virtual string? ApplicationUrl { get; set; }

    public virtual DateTime? PublishedAt { get; set; }

    public virtual JobStatus Status { get; set; }

    public virtual int ViewCount { get; set; }

    public virtual int ApplicationCount { get; set; }

    public virtual int FavoriteCount { get; set; }

    public virtual int ShareCount { get; set; }

    public virtual bool IsFeatured { get; set; }

    public virtual bool IsUrgent { get; set; }

    public virtual bool IsHot { get; set; }

    [NotNull]
    public virtual string SeoTitle { get; set; }

    [CanBeNull]
    public virtual string? SeoDescription { get; set; }

    [CanBeNull]
    public virtual string? SeoKeywords { get; set; }

    public Guid ProvinceId { get; set; }

    public Guid WardId { get; set; }

    public Guid JobCategoryId { get; set; }

    protected JobBase()
    {
    }

    public JobBase(Guid id, Guid provinceId, Guid wardId, Guid jobCategoryId, string title, string slug, JobStatus status, int viewCount, int applicationCount, int favoriteCount, int shareCount, bool isFeatured, bool isUrgent, bool isHot, string seoTitle, string? summary = null, string? description = null, string? requirements = null, string? benefits = null, string? thumbnailUrl = null, string? coverImageUrl = null, EmploymentType? employmentType = null, WorkMode? workMode = null, ExperienceLevel? experienceLevel = null, decimal? salaryMin = null, decimal? salaryMax = null, string? salaryText = null, string? salaryCurrency = null, string? location = null, string? contactEmail = null, string? contactPhone = null, string? applicationUrl = null, DateTime? publishedAt = null, string? seoDescription = null, string? seoKeywords = null)
    {
        Id = id;
        Check.NotNull(title, nameof(title));
        Check.Length(title, nameof(title), JobConsts.TitleMaxLength, 0);
        Check.NotNull(slug, nameof(slug));
        Check.Length(slug, nameof(slug), JobConsts.SlugMaxLength, 0);
        Check.NotNull(seoTitle, nameof(seoTitle));
        Check.Length(seoTitle, nameof(seoTitle), JobConsts.SeoTitleMaxLength, 0);
        Check.Length(summary, nameof(summary), JobConsts.SummaryMaxLength, 0);
        Check.Length(thumbnailUrl, nameof(thumbnailUrl), JobConsts.ThumbnailUrlMaxLength, 0);
        Check.Length(coverImageUrl, nameof(coverImageUrl), JobConsts.CoverImageUrlMaxLength, 0);
        Check.Length(salaryText, nameof(salaryText), JobConsts.SalaryTextMaxLength, 0);
        Check.Length(salaryCurrency, nameof(salaryCurrency), JobConsts.SalaryCurrencyMaxLength, 0);
        Check.Length(location, nameof(location), JobConsts.LocationMaxLength, 0);
        Check.Length(contactEmail, nameof(contactEmail), JobConsts.ContactEmailMaxLength, 0);
        Check.Length(contactPhone, nameof(contactPhone), JobConsts.ContactPhoneMaxLength, 0);
        Check.Length(applicationUrl, nameof(applicationUrl), JobConsts.ApplicationUrlMaxLength, 0);
        Check.Length(seoDescription, nameof(seoDescription), JobConsts.SeoDescriptionMaxLength, 0);
        Check.Length(seoKeywords, nameof(seoKeywords), JobConsts.SeoKeywordsMaxLength, 0);
        Title = title;
        Slug = slug;
        Status = status;
        ViewCount = viewCount;
        ApplicationCount = applicationCount;
        FavoriteCount = favoriteCount;
        ShareCount = shareCount;
        IsFeatured = isFeatured;
        IsUrgent = isUrgent;
        IsHot = isHot;
        SeoTitle = seoTitle;
        Summary = summary;
        Description = description;
        Requirements = requirements;
        Benefits = benefits;
        ThumbnailUrl = thumbnailUrl;
        CoverImageUrl = coverImageUrl;
        EmploymentType = employmentType;
        WorkMode = workMode;
        ExperienceLevel = experienceLevel;
        SalaryMin = salaryMin;
        SalaryMax = salaryMax;
        SalaryText = salaryText;
        SalaryCurrency = salaryCurrency;
        Location = location;
        ContactEmail = contactEmail;
        ContactPhone = contactPhone;
        ApplicationUrl = applicationUrl;
        PublishedAt = publishedAt;
        SeoDescription = seoDescription;
        SeoKeywords = seoKeywords;
        ProvinceId = provinceId;
        WardId = wardId;
        JobCategoryId = jobCategoryId;
    }
}