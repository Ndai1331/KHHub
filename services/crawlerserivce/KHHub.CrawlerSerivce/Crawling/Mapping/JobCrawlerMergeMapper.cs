using System;
using System.Collections.Generic;
using System.Linq;
using KHHub.CrawlerSerivce.Crawling.Models;
using KHHub.MasterDataService.Entities.Jobs;
using KHHub.MasterDataService.Services.Dtos.JobCrawler;

namespace KHHub.CrawlerSerivce.Crawling.Mapping;

/// <summary>
/// Merges listing row + detail patch into MasterData crawler upsert DTO.
/// </summary>
public static class JobCrawlerMergeMapper
{
    private const int SummaryMaxLength = 500;

    public static JobCrawlerUpsertInputDto ToUpsertDto(
        CrawledJobListItem listing,
        CrawledJobDetailPatch detail,
        Guid provinceId,
        Guid wardId,
        Guid jobCategoryId,
        IReadOnlyList<string>? extraTagNames)
    {
        var employment = EmploymentTypeNormalizer.TryMap(detail.EmploymentTypeLabel ?? listing.EmploymentTypeLabel);

        var description = FirstNonEmpty(detail.Description, detail.JobDescriptionHtml);
        var summary = FirstNonEmpty(
                          detail.Summary,
                          TruncatePlain(detail.JobDescriptionHtml != null ? PlainStripHtml(detail.JobDescriptionHtml) : null, SummaryMaxLength),
                          TruncatePlain(description, SummaryMaxLength))
                      ?? TruncatePlain(listing.Title, SummaryMaxLength);

        var salaryText = listing.SalaryDisplayText;
        var salaryMin = detail.SalaryMin;
        var salaryMax = detail.SalaryMax;

        var tags = new List<string>();
        if (extraTagNames != null)
        {
            tags.AddRange(extraTagNames.Where(t => !string.IsNullOrWhiteSpace(t)).Select(t => t.Trim()));
        }

        foreach (var industry in detail.IndustryLabels.Where(x => !string.IsNullOrWhiteSpace(x)))
        {
            foreach (var part in SplitIndustryIntoTagParts(industry))
            {
                tags.Add(part);
            }
        }

        tags = tags.Distinct(StringComparer.OrdinalIgnoreCase).ToList();

        var wardCandidates = detail.RecruitmentLocationLabels
            .Where(s => !string.IsNullOrWhiteSpace(s))
            .Select(s => s.Trim())
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .OrderByDescending(s => s.Length)
            .ToList();

        var primaryCategory = detail.IndustryLabels
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .SelectMany(x => SplitIndustryIntoTagParts(x))
            .FirstOrDefault();

        var location = FirstNonEmpty(
            detail.ContactAddress,
            detail.LocationDetail,
            string.Join(", ", detail.RecruitmentLocationLabels.Where(s => !string.IsNullOrWhiteSpace(s)).Select(s => s.Trim())));

        return new JobCrawlerUpsertInputDto
        {
            ApplicationUrl = listing.SourceAbsoluteUrl,
            Title = listing.Title,
            Summary = summary,
            Description = description,
            Requirements = detail.RequirementsHtml,
            Benefits = detail.BenefitsHtml,
            ThumbnailUrl = detail.ThumbnailUrl,
            CoverImageUrl = FirstNonEmpty(detail.CoverImageUrl, detail.ThumbnailUrl),
            EmploymentType = employment,
            WorkMode = null,
            ExperienceLevel = null,
            SalaryMin = salaryMin,
            SalaryMax = salaryMax,
            SalaryText = salaryText,
            SalaryCurrency = detail.SalaryCurrency ?? "VND",
            Location = TruncateField(location, JobConsts.LocationMaxLength),
            ContactEmail = detail.ContactEmail,
            ContactPhone = detail.ContactPhone,
            Status = JobStatus.Published,
            ProvinceId = provinceId,
            WardId = wardId,
            JobCategoryId = jobCategoryId,
            WardNameCandidates = wardCandidates,
            PrimaryJobCategoryName = primaryCategory,
            TagNames = tags,
            SeoDescription = summary,
            SeoKeywords = BuildIndustryKeywords(detail.IndustryLabels),
            PublishedAt = detail.PublishedAt ?? listing.PublishedAt
        };
    }

    /// <summary>
    /// One source label may contain several keywords separated by '/', e.g. "Điện / Điện tử / Điện lạnh".
    /// </summary>
    private static IEnumerable<string> SplitIndustryIntoTagParts(string raw)
    {
        var trimmed = raw.Trim();
        if (trimmed.Length == 0)
        {
            yield break;
        }

        foreach (var segment in trimmed.Split('/', StringSplitOptions.RemoveEmptyEntries))
        {
            var part = segment.Trim();
            if (part.Length > 0)
            {
                yield return part;
            }
        }
    }

    private static string? BuildIndustryKeywords(List<string> industries)
    {
        var parts = industries
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .SelectMany(x => SplitIndustryIntoTagParts(x))
            .Distinct(StringComparer.OrdinalIgnoreCase)
            .ToList();
        if (parts.Count == 0)
        {
            return null;
        }

        var joined = string.Join(", ", parts);
        return joined.Length <= JobConsts.SeoKeywordsMaxLength
            ? joined
            : joined[..JobConsts.SeoKeywordsMaxLength];
    }

    private static string? TruncateField(string? text, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        var t = text.Trim();
        return t.Length <= maxLength ? t : t[..maxLength];
    }

    private static string? PlainStripHtml(string html)
    {
        if (string.IsNullOrWhiteSpace(html))
        {
            return null;
        }

        var decoded = System.Net.WebUtility.HtmlDecode(html).Trim();
        var stripped = System.Text.RegularExpressions.Regex.Replace(decoded, "<.*?>", " ", System.Text.RegularExpressions.RegexOptions.Singleline);
        return System.Text.RegularExpressions.Regex.Replace(stripped, @"\s+", " ", System.Text.RegularExpressions.RegexOptions.CultureInvariant).Trim();
    }

    private static string? FirstNonEmpty(params string?[] values)
    {
        return values.FirstOrDefault(v => !string.IsNullOrWhiteSpace(v));
    }

    private static string? TruncatePlain(string? text, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return null;
        }

        var plain = text.Trim();
        return plain.Length <= maxLength ? plain : plain[..maxLength];
    }
}
