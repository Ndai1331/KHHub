using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using KHHub.MasterDataService.Data.JobCategories;
using KHHub.MasterDataService.Data.JobTagMappings;
using KHHub.MasterDataService.Data.JobTags;
using KHHub.MasterDataService.Data.Jobs;
using KHHub.MasterDataService.Data.Wards;
using KHHub.MasterDataService.Entities.JobCategories;
using KHHub.MasterDataService.Entities.JobTagMappings;
using KHHub.MasterDataService.Entities.JobTags;
using KHHub.MasterDataService.Entities.Jobs;
using KHHub.MasterDataService.Services.Dtos.JobCrawler;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace KHHub.MasterDataService.IntegrationServices;

public class JobCrawlerIntegrationService : ApplicationService, IJobCrawlerIntegrationService
{
    private readonly IJobRepository _jobRepository;
    private readonly JobManager _jobManager;
    private readonly IJobTagRepository _jobTagRepository;
    private readonly JobTagManager _jobTagManager;
    private readonly IJobTagMappingRepository _jobTagMappingRepository;
    private readonly JobTagMappingManager _jobTagMappingManager;
    private readonly IWardRepository _wardRepository;
    private readonly IJobCategoryRepository _jobCategoryRepository;
    private readonly JobCategoryManager _jobCategoryManager;

    private static readonly Regex NonSlugChars = new(@"[^a-z0-9\-]+", RegexOptions.Compiled);

    public JobCrawlerIntegrationService(
        IJobRepository jobRepository,
        JobManager jobManager,
        IJobTagRepository jobTagRepository,
        JobTagManager jobTagManager,
        IJobTagMappingRepository jobTagMappingRepository,
        JobTagMappingManager jobTagMappingManager,
        IWardRepository wardRepository,
        IJobCategoryRepository jobCategoryRepository,
        JobCategoryManager jobCategoryManager)
    {
        _jobRepository = jobRepository;
        _jobManager = jobManager;
        _jobTagRepository = jobTagRepository;
        _jobTagManager = jobTagManager;
        _jobTagMappingRepository = jobTagMappingRepository;
        _jobTagMappingManager = jobTagMappingManager;
        _wardRepository = wardRepository;
        _jobCategoryRepository = jobCategoryRepository;
        _jobCategoryManager = jobCategoryManager;
    }

    [AllowAnonymous]
    public virtual async Task<JobCrawlerUpsertResultDto> UpsertFromCrawlerAsync(JobCrawlerUpsertInputDto input)
    {
        var existing = await _jobRepository.FirstOrDefaultAsync(j => j.ApplicationUrl == input.ApplicationUrl);

        var wardId = await ResolveWardIdAsync(input.ProvinceId, input.WardNameCandidates, input.WardId);
        var jobCategoryId = await ResolveJobCategoryIdAsync(input.PrimaryJobCategoryName, input.JobCategoryId);

        var slug = BuildSlug(input.Title, input.ApplicationUrl);
        var seoTitle = input.Title.Length > JobConsts.SeoTitleMaxLength
            ? input.Title[..JobConsts.SeoTitleMaxLength]
            : input.Title;

        Job job;
        var wasCreated = false;

        if (existing == null)
        {
            wasCreated = true;
            job = await _jobManager.CreateAsync(
                input.ProvinceId,
                wardId,
                jobCategoryId,
                input.Title,
                slug,
                input.Status,
                viewCount: 0,
                applicationCount: 0,
                favoriteCount: 0,
                shareCount: 0,
                isFeatured: false,
                isUrgent: false,
                isHot: false,
                seoTitle,
                input.Summary,
                input.Description,
                input.Requirements,
                input.Benefits,
                input.ThumbnailUrl,
                input.CoverImageUrl,
                input.EmploymentType,
                input.WorkMode,
                input.ExperienceLevel,
                input.SalaryMin,
                input.SalaryMax,
                input.SalaryText,
                input.SalaryCurrency ?? "VND",
                input.Location,
                input.ContactEmail,
                input.ContactPhone,
                input.ApplicationUrl,
                input.PublishedAt,
                input.SeoDescription ?? input.Summary,
                input.SeoKeywords);
        }
        else
        {
            job = await _jobManager.UpdateAsync(
                existing.Id,
                input.ProvinceId,
                wardId,
                jobCategoryId,
                input.Title,
                slug,
                input.Status,
                existing.ViewCount,
                existing.ApplicationCount,
                existing.FavoriteCount,
                existing.ShareCount,
                existing.IsFeatured,
                existing.IsUrgent,
                existing.IsHot,
                seoTitle,
                input.Summary,
                input.Description,
                input.Requirements,
                input.Benefits,
                input.ThumbnailUrl ?? existing.ThumbnailUrl,
                input.CoverImageUrl ?? existing.CoverImageUrl,
                input.EmploymentType ?? existing.EmploymentType,
                input.WorkMode ?? existing.WorkMode,
                input.ExperienceLevel ?? existing.ExperienceLevel,
                input.SalaryMin ?? existing.SalaryMin,
                input.SalaryMax ?? existing.SalaryMax,
                input.SalaryText ?? existing.SalaryText,
                input.SalaryCurrency ?? existing.SalaryCurrency,
                input.Location ?? existing.Location,
                input.ContactEmail ?? existing.ContactEmail,
                input.ContactPhone ?? existing.ContactPhone,
                input.ApplicationUrl,
                input.PublishedAt ?? existing.PublishedAt,
                input.SeoDescription ?? existing.SeoDescription,
                input.SeoKeywords ?? existing.SeoKeywords,
                existing.ConcurrencyStamp);
        }

        await ReplaceJobTagsAsync(job.Id, input.TagNames);

        return new JobCrawlerUpsertResultDto { JobId = job.Id, WasCreated = wasCreated };
    }

    private async Task<Guid> ResolveWardIdAsync(Guid provinceId, List<string>? candidates, Guid fallbackId)
    {
        if (candidates == null || candidates.Count == 0)
        {
            return fallbackId;
        }

        var queryable = await _wardRepository.GetQueryableAsync();
        var wards = await AsyncExecuter.ToListAsync(queryable.Where(w => w.ProvinceId == provinceId));
        if (wards.Count == 0)
        {
            return fallbackId;
        }

        var ordered = candidates
            .Where(c => !string.IsNullOrWhiteSpace(c))
            .Select(c => c.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(c => ComparableAsciiKey(c).Length)
            .ToList();

        foreach (var c in ordered)
        {
            var ck = ComparableAsciiKey(c);
            if (ck.Length == 0)
            {
                continue;
            }

            var hit = wards.FirstOrDefault(w => ComparableAsciiKey(w.Name) == ck);
            if (hit != null)
            {
                return hit.Id;
            }
        }

        foreach (var c in ordered)
        {
            var ck = ComparableAsciiKey(c);
            if (ck.Length < 6)
            {
                continue;
            }

            var hit = wards.FirstOrDefault(w => ComparableAsciiKey(w.Name).Contains(ck, StringComparison.Ordinal));
            if (hit != null)
            {
                return hit.Id;
            }
        }

        return fallbackId;
    }

    private async Task<Guid> ResolveJobCategoryIdAsync(string? primaryName, Guid fallbackId)
    {
        if (string.IsNullOrWhiteSpace(primaryName))
        {
            return fallbackId;
        }

        var trimmed = primaryName.Trim();
        var queryable = await _jobCategoryRepository.GetQueryableAsync();
        var categories = await AsyncExecuter.ToListAsync(queryable.Where(c => c.IsActive));
        var key = ComparableAsciiKey(trimmed);
        var existing = categories.FirstOrDefault(c => ComparableAsciiKey(c.Name) == key);
        if (existing != null)
        {
            return existing.Id;
        }

        var slug = BuildJobCategorySeoSlug(trimmed);
        var existingBySlug = categories.FirstOrDefault(c =>
            string.Equals(c.Slug, slug, StringComparison.OrdinalIgnoreCase));
        if (existingBySlug != null)
        {
            return existingBySlug.Id;
        }

        var nextOrder = categories.Count == 0 ? 1 : categories.Max(c => c.DisplayOrder) + 1;
        var created = await _jobCategoryManager.CreateAsync(trimmed, slug, nextOrder, true);
        return created.Id;
    }

    private static string ComparableAsciiKey(string text)
    {
        return SlugifyAsciiSegment(text).Replace("-", "", StringComparison.Ordinal);
    }

    private static string BuildJobCategorySeoSlug(string name)
    {
        var ascii = SlugifyAsciiSegment(name);
        if (string.IsNullOrEmpty(ascii))
        {
            ascii = "job-category";
        }

        return ascii.Length > JobCategoryConsts.SlugMaxLength
            ? ascii[..JobCategoryConsts.SlugMaxLength]
            : ascii;
    }

    private async Task ReplaceJobTagsAsync(Guid jobId, List<string> tagNames)
    {
        var queryable = await _jobTagMappingRepository.GetQueryableAsync();
        var mappingIds = queryable.Where(m => m.JobId == jobId).Select(m => m.Id).ToList();
        await _jobTagMappingRepository.DeleteManyAsync(mappingIds);

        foreach (var rawName in tagNames.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var name = rawName.Trim();
            var tagSlug = BuildTagSlug(name);
            var tag = await _jobTagRepository.FirstOrDefaultAsync(t => t.Slug == tagSlug);
            if (tag == null)
            {
                tag = await _jobTagManager.CreateAsync(name, tagSlug, usageCount: 0);
            }

            await _jobTagMappingManager.CreateAsync(tag.Id, jobId, isPrimary: false, sortOrder: "1");
        }
    }

    private static string BuildSlug(string title, string applicationUrl)
    {
        var ascii = SlugifyAsciiSegment(title);
        var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(applicationUrl))).ToLowerInvariant();
        var combined = $"{ascii}-{hash[..16]}".Trim('-');
        if (combined.Length > JobConsts.SlugMaxLength)
        {
            combined = combined[..JobConsts.SlugMaxLength];
        }

        return combined;
    }

    private static string BuildTagSlug(string name)
    {
        var ascii = SlugifyAsciiSegment(name);
        if (string.IsNullOrEmpty(ascii))
        {
            ascii = "tag";
        }

        var hash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(name))).ToLowerInvariant();
        var combined = $"{ascii}-{hash[..8]}";
        if (combined.Length > JobTagConsts.SlugMaxLength)
        {
            combined = combined[..JobTagConsts.SlugMaxLength];
        }

        return combined;
    }

    private static string SlugifyAsciiSegment(string text)
    {
        var lowered = text.Trim().ToLowerInvariant();
        var sb = new StringBuilder(lowered.Length);
        foreach (var c in lowered)
        {
            sb.Append(c switch
            {
                'à' or 'á' or 'ạ' or 'ả' or 'ã' or 'â' or 'ầ' or 'ấ' or 'ậ' or 'ẩ' or 'ẫ' or 'ă' or 'ằ' or 'ắ' or 'ặ' or 'ẳ' or 'ẵ' => 'a',
                'è' or 'é' or 'ẹ' or 'ẻ' or 'ẽ' or 'ê' or 'ề' or 'ế' or 'ệ' or 'ể' or 'ễ' => 'e',
                'ì' or 'í' or 'ị' or 'ỉ' or 'ĩ' => 'i',
                'ò' or 'ó' or 'ọ' or 'ỏ' or 'õ' or 'ô' or 'ồ' or 'ố' or 'ộ' or 'ổ' or 'ỗ' or 'ơ' or 'ờ' or 'ớ' or 'ợ' or 'ở' or 'ỡ' => 'o',
                'ù' or 'ú' or 'ụ' or 'ủ' or 'ũ' or 'ư' or 'ừ' or 'ứ' or 'ự' or 'ử' or 'ữ' => 'u',
                'ỳ' or 'ý' or 'ỵ' or 'ỷ' or 'ỹ' => 'y',
                'đ' => 'd',
                ' ' or '_' => '-',
                _ when char.IsLetterOrDigit(c) => c,
                _ when c == '-' => '-',
                _ => ' '
            });
        }

        var collapsed = NonSlugChars.Replace(sb.ToString().Replace(" ", "-"), "-");
        while (collapsed.Contains("--", StringComparison.Ordinal))
        {
            collapsed = collapsed.Replace("--", "-", StringComparison.Ordinal);
        }

        return collapsed.Trim('-');
    }
}
