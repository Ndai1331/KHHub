using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;
using KHHub.MasterDataService.Data.Jobs;

namespace KHHub.MasterDataService.Entities.Jobs;

public abstract class JobManagerBase : DomainService
{
    protected IJobRepository _jobRepository;

    public JobManagerBase(IJobRepository jobRepository)
    {
        _jobRepository = jobRepository;
    }

    public virtual async Task<Job> CreateAsync(Guid provinceId, Guid wardId, Guid jobCategoryId, string title, string slug, JobStatus status, int viewCount, int applicationCount, int favoriteCount, int shareCount, bool isFeatured, bool isUrgent, bool isHot, string seoTitle, string? summary = null, string? description = null, string? requirements = null, string? benefits = null, string? thumbnailUrl = null, string? coverImageUrl = null, EmploymentType? employmentType = null, WorkMode? workMode = null, ExperienceLevel? experienceLevel = null, decimal? salaryMin = null, decimal? salaryMax = null, string? salaryText = null, string? salaryCurrency = null, string? location = null, string? contactEmail = null, string? contactPhone = null, string? applicationUrl = null, DateTime? publishedAt = null, string? seoDescription = null, string? seoKeywords = null)
    {
        Check.NotNull(provinceId, nameof(provinceId));
        Check.NotNull(wardId, nameof(wardId));
        Check.NotNull(jobCategoryId, nameof(jobCategoryId));
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.Length(title, nameof(title), JobConsts.TitleMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), JobConsts.SlugMaxLength);
        Check.NotNull(status, nameof(status));
        Check.NotNullOrWhiteSpace(seoTitle, nameof(seoTitle));
        Check.Length(seoTitle, nameof(seoTitle), JobConsts.SeoTitleMaxLength);
        Check.Length(summary, nameof(summary), JobConsts.SummaryMaxLength);
        Check.Length(thumbnailUrl, nameof(thumbnailUrl), JobConsts.ThumbnailUrlMaxLength);
        Check.Length(coverImageUrl, nameof(coverImageUrl), JobConsts.CoverImageUrlMaxLength);
        Check.Length(salaryText, nameof(salaryText), JobConsts.SalaryTextMaxLength);
        Check.Length(salaryCurrency, nameof(salaryCurrency), JobConsts.SalaryCurrencyMaxLength);
        Check.Length(location, nameof(location), JobConsts.LocationMaxLength);
        Check.Length(contactEmail, nameof(contactEmail), JobConsts.ContactEmailMaxLength);
        Check.Length(contactPhone, nameof(contactPhone), JobConsts.ContactPhoneMaxLength);
        Check.Length(applicationUrl, nameof(applicationUrl), JobConsts.ApplicationUrlMaxLength);
        Check.Length(seoDescription, nameof(seoDescription), JobConsts.SeoDescriptionMaxLength);
        Check.Length(seoKeywords, nameof(seoKeywords), JobConsts.SeoKeywordsMaxLength);
        var job = new Job(GuidGenerator.Create(), provinceId, wardId, jobCategoryId, title, slug, status, viewCount, applicationCount, favoriteCount, shareCount, isFeatured, isUrgent, isHot, seoTitle, summary, description, requirements, benefits, thumbnailUrl, coverImageUrl, employmentType, workMode, experienceLevel, salaryMin, salaryMax, salaryText, salaryCurrency, location, contactEmail, contactPhone, applicationUrl, publishedAt, seoDescription, seoKeywords);
        return await _jobRepository.InsertAsync(job);
    }

    public virtual async Task<Job> UpdateAsync(Guid id, Guid provinceId, Guid wardId, Guid jobCategoryId, string title, string slug, JobStatus status, int viewCount, int applicationCount, int favoriteCount, int shareCount, bool isFeatured, bool isUrgent, bool isHot, string seoTitle, string? summary = null, string? description = null, string? requirements = null, string? benefits = null, string? thumbnailUrl = null, string? coverImageUrl = null, EmploymentType? employmentType = null, WorkMode? workMode = null, ExperienceLevel? experienceLevel = null, decimal? salaryMin = null, decimal? salaryMax = null, string? salaryText = null, string? salaryCurrency = null, string? location = null, string? contactEmail = null, string? contactPhone = null, string? applicationUrl = null, DateTime? publishedAt = null, string? seoDescription = null, string? seoKeywords = null, [CanBeNull] string? concurrencyStamp = null)
    {
        Check.NotNull(provinceId, nameof(provinceId));
        Check.NotNull(wardId, nameof(wardId));
        Check.NotNull(jobCategoryId, nameof(jobCategoryId));
        Check.NotNullOrWhiteSpace(title, nameof(title));
        Check.Length(title, nameof(title), JobConsts.TitleMaxLength);
        Check.NotNullOrWhiteSpace(slug, nameof(slug));
        Check.Length(slug, nameof(slug), JobConsts.SlugMaxLength);
        Check.NotNull(status, nameof(status));
        Check.NotNullOrWhiteSpace(seoTitle, nameof(seoTitle));
        Check.Length(seoTitle, nameof(seoTitle), JobConsts.SeoTitleMaxLength);
        Check.Length(summary, nameof(summary), JobConsts.SummaryMaxLength);
        Check.Length(thumbnailUrl, nameof(thumbnailUrl), JobConsts.ThumbnailUrlMaxLength);
        Check.Length(coverImageUrl, nameof(coverImageUrl), JobConsts.CoverImageUrlMaxLength);
        Check.Length(salaryText, nameof(salaryText), JobConsts.SalaryTextMaxLength);
        Check.Length(salaryCurrency, nameof(salaryCurrency), JobConsts.SalaryCurrencyMaxLength);
        Check.Length(location, nameof(location), JobConsts.LocationMaxLength);
        Check.Length(contactEmail, nameof(contactEmail), JobConsts.ContactEmailMaxLength);
        Check.Length(contactPhone, nameof(contactPhone), JobConsts.ContactPhoneMaxLength);
        Check.Length(applicationUrl, nameof(applicationUrl), JobConsts.ApplicationUrlMaxLength);
        Check.Length(seoDescription, nameof(seoDescription), JobConsts.SeoDescriptionMaxLength);
        Check.Length(seoKeywords, nameof(seoKeywords), JobConsts.SeoKeywordsMaxLength);
        var job = await _jobRepository.GetAsync(id);
        job.ProvinceId = provinceId;
        job.WardId = wardId;
        job.JobCategoryId = jobCategoryId;
        job.Title = title;
        job.Slug = slug;
        job.Status = status;
        job.ViewCount = viewCount;
        job.ApplicationCount = applicationCount;
        job.FavoriteCount = favoriteCount;
        job.ShareCount = shareCount;
        job.IsFeatured = isFeatured;
        job.IsUrgent = isUrgent;
        job.IsHot = isHot;
        job.SeoTitle = seoTitle;
        job.Summary = summary;
        job.Description = description;
        job.Requirements = requirements;
        job.Benefits = benefits;
        job.ThumbnailUrl = thumbnailUrl;
        job.CoverImageUrl = coverImageUrl;
        job.EmploymentType = employmentType;
        job.WorkMode = workMode;
        job.ExperienceLevel = experienceLevel;
        job.SalaryMin = salaryMin;
        job.SalaryMax = salaryMax;
        job.SalaryText = salaryText;
        job.SalaryCurrency = salaryCurrency;
        job.Location = location;
        job.ContactEmail = contactEmail;
        job.ContactPhone = contactPhone;
        job.ApplicationUrl = applicationUrl;
        job.PublishedAt = publishedAt;
        job.SeoDescription = seoDescription;
        job.SeoKeywords = seoKeywords;
        job.SetConcurrencyStampIfNotNull(concurrencyStamp);
        return await _jobRepository.UpdateAsync(job);
    }
}