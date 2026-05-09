using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using KHHub.MasterDataService.Entities.Jobs;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Services.Dtos.HomeBanners;
using KHHub.MasterDataService.Services.Dtos.JobTagMappings;
using KHHub.MasterDataService.Services.Dtos.Places;
using KHHub.MasterDataService.Services.Dtos.Jobs;
using KHHub.Publish_website.Services.PublicContent;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KHHub.Publish_website.Services;

/// <summary>
/// Loads public catalog data from MasterData via the Web Gateway (YARP).
/// </summary>
public class PublicMasterDataCatalogClient
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };

    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    private readonly ILogger<PublicMasterDataCatalogClient> _logger;

    public PublicMasterDataCatalogClient(
        HttpClient httpClient,
        IConfiguration configuration,
        ILogger<PublicMasterDataCatalogClient> logger)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<IReadOnlyList<HomeBannerDto>> GetActiveHomeBannersAsync(
        int maxCount,
        DateTime now,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var sorting = Uri.EscapeDataString("SortOrder asc");
            var path = $"api/masterdata/home-banners?skipCount=0&maxResultCount={maxCount}&sorting={sorting}&isActive=true";
            using var response = await _httpClient.GetAsync(path, cancellationToken);
            response.EnsureSuccessStatusCode();
            var page = await response.Content.ReadFromJsonAsync<PagedResult<HomeBannerDto>>(JsonOptions, cancellationToken);
            if (page?.Items == null || page.Items.Count == 0)
            {
                return Array.Empty<HomeBannerDto>();
            }

            return page.Items
                .Where(b =>
                    (!b.StartDate.HasValue || b.StartDate.Value <= now) &&
                    (!b.EndDate.HasValue || b.EndDate.Value >= now))
                .OrderBy(b => b.SortOrder)
                .Take(maxCount)
                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to load home banners from MasterData gateway.");
            return Array.Empty<HomeBannerDto>();
        }
    }

    public async Task<IReadOnlyList<PlaceWithNavigationPropertiesDto>> GetPublishedPlacesAsync(
        int maxCount,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var sorting = Uri.EscapeDataString("RatingAveraged desc,ViewCount desc");
            var status = (int)PlaceStatus.Published;
            var path =
                $"api/masterdata/places?skipCount=0&maxResultCount={maxCount}&sorting={sorting}&status={status}";
            using var response = await _httpClient.GetAsync(path, cancellationToken);
            response.EnsureSuccessStatusCode();
            var page = await response.Content.ReadFromJsonAsync<PagedResult<PlaceWithNavigationPropertiesDto>>(
                JsonOptions,
                cancellationToken);
            return page?.Items is { Count: > 0 }
                ? page.Items
                : Array.Empty<PlaceWithNavigationPropertiesDto>();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to load places from MasterData gateway.");
            return Array.Empty<PlaceWithNavigationPropertiesDto>();
        }
    }

    public async Task<PublicJobListingResult> GetPublishedJobListingAsync(
        PublicContentQuery query,
        string actionPath,
        CancellationToken cancellationToken = default)
    {
        var jobs = await GetPublishedJobsAsync(1000, cancellationToken);
        var tagsByJobId = await GetTagsByJobIdAsync(cancellationToken);
        var cards = jobs.Select(job => ToJobCard(job, tagsByJobId, actionPath)).ToList();
        var filteredCards = ApplyJobFilters(cards, query);
        var sortedCards = ApplyJobSort(filteredCards, query.Sort);

        var page = Math.Max(1, query.Page);
        var pageSize = Math.Clamp(query.PageSize, 1, 20);

        return new PublicJobListingResult
        {
            Cards = new PagedContentResult<PublicContentCardViewModel>
            {
                Items = sortedCards.Skip((page - 1) * pageSize).Take(pageSize).ToList(),
                TotalCount = sortedCards.Count,
                CurrentPage = page,
                PageSize = pageSize
            },
            Filters = new FilterSidebarViewModel
            {
                ActionPath = actionPath,
                Title = "Bộ lọc",
                Query = query,
                Provinces = BuildOptions(cards.Where(x => !string.IsNullOrWhiteSpace(x.Province)).GroupBy(x => x.Province!)),
                Wards = BuildOptions(cards.Where(x => !string.IsNullOrWhiteSpace(x.Ward)).GroupBy(x => x.Ward!)),
                Categories = BuildOptions(cards.GroupBy(x => x.Category)),
                Tags = BuildTagOptions(cards)
            }
        };
    }

    public async Task<IReadOnlyList<PublicContentCardViewModel>> GetLatestPublishedJobCardsAsync(
        int maxCount,
        string detailBasePath,
        CancellationToken cancellationToken = default)
    {
        var jobs = await GetPublishedJobsAsync(maxCount, cancellationToken);
        var tagsByJobId = await GetTagsByJobIdAsync(cancellationToken);

        return jobs
            .Select(job => ToJobCard(job, tagsByJobId, detailBasePath))
            .Take(maxCount)
            .ToList();
    }

    public async Task<DetailPageViewModel?> GetPublishedJobDetailAsync(
        string slug,
        string detailBasePath,
        CancellationToken cancellationToken = default)
    {
        var normalizedSlug = (slug ?? string.Empty).Trim();
        if (normalizedSlug.Length == 0)
        {
            return null;
        }

        var jobs = await GetPublishedJobsAsync(1000, cancellationToken);
        var job = jobs.FirstOrDefault(x =>
            string.Equals(x.Job.Slug, normalizedSlug, StringComparison.OrdinalIgnoreCase));

        if (job == null)
        {
            return null;
        }

        var tagsByJobId = await GetTagsByJobIdAsync(cancellationToken);
        var currentTags = GetTagValues(job.Job.Id, tagsByJobId);
        var related = jobs
            .Where(x => x.Job.Id != job.Job.Id)
            .Select(x => ToJobCard(x, tagsByJobId, detailBasePath))
            .OrderByDescending(x => string.Equals(x.Category, job.JobCategory?.Name, StringComparison.OrdinalIgnoreCase))
            .ThenByDescending(x => x.Tags.Intersect(currentTags, StringComparer.OrdinalIgnoreCase).Count())
            .ThenByDescending(x => x.Popularity)
            .Take(3)
            .ToList();

        return new DetailPageViewModel
        {
            Kind = PublicContentKind.Job,
            Title = job.Job.Title,
            Description = FirstNonEmpty(job.Job.Summary, job.Job.SeoDescription, job.Job.Description, job.Job.Title),
            ContentHtml = BuildJobDescriptionHtml(job.Job),
            Category = job.JobCategory?.Name ?? "Việc làm",
            ThumbnailUrl = ResolveBrowserImageUrl(job.Job.ThumbnailUrl ?? job.Job.CoverImageUrl, job.Job.Slug),
            CreatedAt = ToDateTimeOffset(job.Job.PublishedAt, job.Job.CreationTime),
            AuthorOrCompany = string.IsNullOrWhiteSpace(job.Job.Location) ? "KH HUB" : job.Job.Location,
            Salary = FormatSalary(job.Job),
            Requirements = job.Job.Requirements,
            Province = job.Province?.Name,
            Ward = job.Ward?.Name,
            Rating = 4.5m,
            Tags = currentTags,
            RelatedItems = related
        };
    }

    private async Task<IReadOnlyList<JobWithNavigationPropertiesDto>> GetPublishedJobsAsync(
        int maxCount,
        CancellationToken cancellationToken)
    {
        try
        {
            var sorting = Uri.EscapeDataString("PublishedAt desc,CreationTime desc");
            var status = (int)JobStatus.Published;
            var path = $"api/masterdata/jobs?skipCount=0&maxResultCount={maxCount}&sorting={sorting}&status={status}";
            using var response = await _httpClient.GetAsync(path, cancellationToken);
            response.EnsureSuccessStatusCode();
            var page = await response.Content.ReadFromJsonAsync<PagedResult<JobWithNavigationPropertiesDto>>(
                JsonOptions,
                cancellationToken);

            return page?.Items is { Count: > 0 }
                ? page.Items
                : Array.Empty<JobWithNavigationPropertiesDto>();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to load jobs from MasterData gateway.");
            return Array.Empty<JobWithNavigationPropertiesDto>();
        }
    }

    private async Task<IReadOnlyDictionary<Guid, IReadOnlyList<string>>> GetTagsByJobIdAsync(
        CancellationToken cancellationToken)
    {
        try
        {
            var sorting = Uri.EscapeDataString("SortOrder asc");
            var path = $"api/masterdata/job-tag-mappings?skipCount=0&maxResultCount=1000&sorting={sorting}";
            using var response = await _httpClient.GetAsync(path, cancellationToken);
            response.EnsureSuccessStatusCode();
            var page = await response.Content.ReadFromJsonAsync<PagedResult<JobTagMappingWithNavigationPropertiesDto>>(
                JsonOptions,
                cancellationToken);

            if (page?.Items is not { Count: > 0 })
            {
                return new Dictionary<Guid, IReadOnlyList<string>>();
            }

            return page.Items
                .Where(x => x.JobTagMapping != null && x.JobTag != null)
                .GroupBy(x => x.JobTagMapping.JobId)
                .ToDictionary(
                    x => x.Key,
                    x => (IReadOnlyList<string>)x
                        .Select(item => FirstNonEmpty(item.JobTag.Slug, item.JobTag.Name))
                        .Where(tag => !string.IsNullOrWhiteSpace(tag))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList());
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to load job tags from MasterData gateway.");
            return new Dictionary<Guid, IReadOnlyList<string>>();
        }
    }

    private PublicContentCardViewModel ToJobCard(
        JobWithNavigationPropertiesDto item,
        IReadOnlyDictionary<Guid, IReadOnlyList<string>> tagsByJobId,
        string detailBasePath)
    {
        var job = item.Job;
        var tags = GetTagValues(job.Id, tagsByJobId);
        var province = item.Province?.Name;
        var ward = item.Ward?.Name;

        return new PublicContentCardViewModel
        {
            Kind = PublicContentKind.Job,
            Title = job.Title,
            Slug = job.Slug,
            EnglishSlug = job.Slug,
            Url = $"{detailBasePath.TrimEnd('/')}/{job.Slug}",
            Description = FirstNonEmpty(job.Summary, job.SeoDescription, StripHtml(job.Description), job.Title),
            ThumbnailUrl = ResolveBrowserImageUrl(job.ThumbnailUrl ?? job.CoverImageUrl, job.Slug),
            Category = item.JobCategory?.Name ?? "Việc làm",
            Province = province,
            Ward = ward,
            MetaLabel = FormatJobMeta(job),
            CreatedAt = ToDateTimeOffset(job.PublishedAt, job.CreationTime),
            Popularity = job.ViewCount,
            Tags = tags
        };
    }

    private static List<PublicContentCardViewModel> ApplyJobFilters(
        IEnumerable<PublicContentCardViewModel> cards,
        PublicContentQuery query)
    {
        var filtered = cards;
        if (!string.IsNullOrWhiteSpace(query.Search))
        {
            filtered = filtered.Where(x =>
                x.Title.Contains(query.Search, StringComparison.OrdinalIgnoreCase) ||
                x.Description.Contains(query.Search, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.Province))
        {
            filtered = filtered.Where(x => string.Equals(x.Province, query.Province, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.Ward))
        {
            filtered = filtered.Where(x => string.Equals(x.Ward, query.Ward, StringComparison.OrdinalIgnoreCase));
        }

        if (!string.IsNullOrWhiteSpace(query.Category))
        {
            filtered = filtered.Where(x => string.Equals(x.Category, query.Category, StringComparison.OrdinalIgnoreCase));
        }

        var tags = query.Tags.Where(x => !string.IsNullOrWhiteSpace(x)).ToHashSet(StringComparer.OrdinalIgnoreCase);
        if (tags.Count > 0)
        {
            filtered = filtered.Where(x => x.Tags.Any(tags.Contains));
        }

        return filtered.ToList();
    }

    private static List<PublicContentCardViewModel> ApplyJobSort(
        IEnumerable<PublicContentCardViewModel> cards,
        string? sort)
    {
        return (sort ?? PublicContentSort.Newest).ToLowerInvariant() switch
        {
            PublicContentSort.Popular => cards.OrderByDescending(x => x.Popularity).ToList(),
            PublicContentSort.Relevant => cards
                .OrderByDescending(x => x.Tags.Count)
                .ThenByDescending(x => x.Popularity)
                .ToList(),
            _ => cards.OrderByDescending(x => x.CreatedAt).ToList()
        };
    }

    private static IReadOnlyList<FilterOption> BuildOptions(
        IEnumerable<IGrouping<string, PublicContentCardViewModel>> groups)
    {
        return groups
            .OrderBy(x => x.Key)
            .Select(x => new FilterOption(x.Key, x.Key, x.Count()))
            .ToList();
    }

    private static IReadOnlyList<FilterOption> BuildTagOptions(
        IReadOnlyList<PublicContentCardViewModel> cards)
    {
        return cards
            .SelectMany(x => x.Tags)
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .GroupBy(x => x, StringComparer.OrdinalIgnoreCase)
            .OrderBy(x => x.Key)
            .Select(x => new FilterOption(x.Key, x.Key, x.Count()))
            .ToList();
    }

    private static IReadOnlyList<string> GetTagValues(
        Guid jobId,
        IReadOnlyDictionary<Guid, IReadOnlyList<string>> tagsByJobId)
    {
        return tagsByJobId.TryGetValue(jobId, out var tags)
            ? tags
            : Array.Empty<string>();
    }

    private string ResolveBrowserImageUrl(string? imageUrl, string seed)
    {
        var value = (imageUrl ?? string.Empty).Trim();
        if (value.Length == 0)
        {
            return FallbackJobImage(seed);
        }

        if (value.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
            value.StartsWith("https://", StringComparison.OrdinalIgnoreCase) ||
            value.StartsWith("//", StringComparison.Ordinal))
        {
            return value;
        }

        var baseUrl = (_configuration["MediaFiles:PublicBaseUrl"] ?? string.Empty).Trim().TrimEnd('/');
        if (string.IsNullOrWhiteSpace(baseUrl))
        {
            return value.StartsWith('/') ? value : "/" + value;
        }

        if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out var baseUri))
        {
            return value;
        }

        return baseUri.GetLeftPart(UriPartial.Authority) + "/" + value.TrimStart('/');
    }

    private static string FormatJobMeta(JobDto job)
    {
        var parts = new[]
        {
            FormatSalary(job),
            job.EmploymentType?.ToString(),
            job.WorkMode?.ToString()
        }.Where(x => !string.IsNullOrWhiteSpace(x));

        return string.Join(" · ", parts);
    }

    private static string FormatSalary(JobDto job)
    {
        if (!string.IsNullOrWhiteSpace(job.SalaryText))
        {
            return job.SalaryText;
        }

        if (job.SalaryMin.HasValue && job.SalaryMax.HasValue)
        {
            return $"{job.SalaryMin:0,0} - {job.SalaryMax:0,0} {FirstNonEmpty(job.SalaryCurrency, "VND")}";
        }

        if (job.SalaryMin.HasValue)
        {
            return $"Từ {job.SalaryMin:0,0} {FirstNonEmpty(job.SalaryCurrency, "VND")}";
        }

        if (job.SalaryMax.HasValue)
        {
            return $"Đến {job.SalaryMax:0,0} {FirstNonEmpty(job.SalaryCurrency, "VND")}";
        }

        return "Thỏa thuận";
    }

    private static string BuildJobDescriptionHtml(JobDto job)
    {
        var description = FirstNonEmpty(job.Description, job.Summary, job.SeoDescription);
        if (string.IsNullOrWhiteSpace(description))
        {
            return "<p>Thông tin chi tiết sẽ được cập nhật.</p>";
        }

        return LooksLikeHtml(description)
            ? description
            : $"<p>{WebUtility.HtmlEncode(description)}</p>";
    }

    private static string StripHtml(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        return WebUtility.HtmlDecode(System.Text.RegularExpressions.Regex.Replace(value, "<.*?>", string.Empty)).Trim();
    }

    private static bool LooksLikeHtml(string value)
    {
        return value.Contains('<', StringComparison.Ordinal) && value.Contains('>', StringComparison.Ordinal);
    }

    private static DateTimeOffset ToDateTimeOffset(DateTime? preferred, DateTime fallback)
    {
        var value = preferred ?? fallback;
        return new DateTimeOffset(DateTime.SpecifyKind(value, DateTimeKind.Utc));
    }

    private static string FirstNonEmpty(params string?[] values)
    {
        return values.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x)) ?? string.Empty;
    }

    private static string FallbackJobImage(string seed)
    {
        var seedSlug = Uri.EscapeDataString($"khhub-job-{seed}".ToLowerInvariant());
        return $"https://picsum.photos/seed/{seedSlug}/1200/720";
    }
}

public class PagedResult<T>
{
    public List<T> Items { get; set; } = [];

    public long TotalCount { get; set; }
}
